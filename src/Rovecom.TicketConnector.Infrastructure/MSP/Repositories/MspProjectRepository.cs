using Dapper;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.MSP.MspProjectEntity;
using Rovecom.TicketConnector.Domain.MSP.MspRequestEntity;
using Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity;
using System;
using System.Collections.Generic;
using System.Data;

namespace Rovecom.TicketConnector.Infrastructure.MSP.Repositories
{
    public class MspProjectRepository : IMspProjectRepository
    {
        /// <summary>
        /// The connection to the database
        /// </summary>
        private IDbConnection Connection => Transaction.Connection;

        /// <summary>
        /// The transaction with the database
        /// </summary>
        private IDbTransaction Transaction { get; }

        /// <inheritdoc />
        public MspProjectRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        /// <inheritdoc />
        public IEnumerable<MspProject> GetAll()
        {
            var projects = new List<MspProject>();

            var result = Connection.Query(
                "SELECT wf.workorderid, wf.udf_char1, wo.title, wo.description, wo.siteid, cs.technicianid, cs.ts_starttime, cs.ts_endtime, cs.worklogtypeid, wlf.udf_long1 " +
                "FROM workorder_fields as wf " +
                "LEFT JOIN workorder as wo ON wf.workorderid = wo.workorderid " +
                "LEFT JOIN workordertocharge as wotc ON wf.workorderid = wotc.workorderid " +
                "LEFT JOIN chargestable as cs ON wotc.chargeid = cs.chargeid " +
                "LEFT JOIN worklog_fields as wlf ON cs.chargeid = wlf.worklogid " +
                "ORDER BY udf_char1", transaction: Transaction);

            MspProject project = null;
            MspRequest currentRequest = null;

            foreach (var res in result)
            {
                // Requests that don't belong to a project are skipped
                if (string.IsNullOrEmpty(res.udf_char1))
                    continue;

                // Create a new project when a new project code is encountered
                if (project == null || !string.Equals(res.udf_char1, project.Code))
                {
                    project = new MspProject(0, res.udf_char1)
                    {
                        Code = res.udf_char1,
                        Requests = new List<MspRequest>()
                    };
                    projects.Add(project);
                }

                // Create a new request when a new request title is encountered
                if (currentRequest == null || !string.Equals(res.title, currentRequest.Title))
                {
                    currentRequest = new MspRequest(res.title, res.description, res.siteid);
                    project.AddRequest(currentRequest);
                }

                var worklog = new MspWorklog(
                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(res.ts_starttime),
                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(res.ts_endtime),
                    res.description,
                    res.udf_long1,
                    res.technicianid
                );

                currentRequest.AddWorklog(worklog);
            }

            return projects;
        }

        /// <inheritdoc />
        public void Add(MspProject project)
        {
            AddProjectCode(project);

            foreach (var request in project.Requests)
            {
                AddRequest(request, project.Code);
            }
        }

        /// <inheritdoc />
        public void Update(IProject project)
        {
            if (!(project is MspProject mspProject))
                return;

            foreach (var request in mspProject.Requests)
            {
                UpdateRequest(request);
            }
        }

        private void UpdateRequest(MspRequest request)
        {
            const string query = "UPDATE workorder SET title = @Title, description = @Description WHERE workorderid = @WorkorderId";

            var param = new
            {
                Title = request.Title,
                Description = request.Description
            };

            Connection.Execute(query, param, Transaction);

            foreach (var worklog in request.Worklogs)
            {
                AddOrUpdateWorklog(worklog, request.Id);
            }
        }

        private void AddOrUpdateWorklog(MspWorklog worklog, long requestId)
        {
            if (worklog.Id == 0)
            {
                AddWorklog(worklog, requestId, 2601);
            }
            else
            {
                UpdateWorklog(worklog);
            }
        }

        private void UpdateWorklog(MspWorklog worklog)
        {
            const string query =
                "UPDATE chargestable SET technicianid = @TechId, ts_starttime = @StartTime, ts_endtime = @EndTime, timespent = @TimeSpend, worklogtypeid = @WorklogtypeId " +
                "WHERE workorderid = @WorkorderId";

            var param = new
            {
                TechId = 2601,
                StartTime = worklog.WorkStartedDateTime,
                EndTime = worklog.WorkEndedDateTime,
                TimeSpend = (worklog.WorkEndedDateTime - worklog.WorkStartedDateTime).Minutes * 60 * 1000,
                WorklogtypeId = 0,
                WorkorderId = worklog.Id
            };

            Connection.Execute(query, param, Transaction);
        }

        private void AddRequest(MspRequest request, string projectCode)
        {
            // Add request
            const string requestQuery =
                "INSERT INTO workorder(workorderid, requesterid, createdbyid, createdtime, title, description) " +
                "VALUES (nextval('workorderid_seq'), @RequesterId, @CreatedbyId, @CreatedTime, @Title, @Description) RETURNING workorderid";

            var requestParam = new
            {
                RequesterId = 2702,
                CreatedbyId = 2702,
                CreatedTime = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Title = request.Title,
                Description = request.Description
            };

            request.Id = Connection.ExecuteScalar<long>(requestQuery, requestParam, Transaction);

            // Add project code to request
            const string query =
                "INSERT INTO workorder_fields(workorderid, udf_char1) " +
                "VALUES (@RequestId, @ProjectCode)";

            var param = new
            {
                RequestId = request.Id,
                ProjectCode = projectCode
            };
            Connection.Execute(query, param, Transaction);

            // Add worklogs
            foreach (var worklog in request.Worklogs)
            {
                AddWorklog(worklog, request.Id, 2601);
            }
        }

        private void AddWorklog(MspWorklog worklog, long requestId, long technicianId)
        {
            // Creates the worklog in MSP
            const string worklogQuery =
                "INSERT INTO chargestable(chargeid, technicianid, createdby, description, timespent, ts_starttime, ts_endtime, worklogtypeid) " +
                "VALUES (nextval('chargeid_seq'), @TechnicianId, @TechnicianId, @Description, @TimeSpent, @StartTime, @EndTime, @WorklogTypeId) RETURNING chargeid";

            var worklogParam = new
            {
                TechnicianId = technicianId,
                Description = worklog.Description,
                TimeSpent = (worklog.WorkEndedDateTime - worklog.WorkStartedDateTime).Minutes * 60 * 1000,
                StartTime = worklog.WorkStartedDateTime,
                EndTime = worklog.WorkEndedDateTime,
                WorklogTypeId = 1
            };

            worklog.Id = Connection.ExecuteScalar<long>(worklogQuery, worklogParam, Transaction);

            // Adds extra field values to MSP db
            const string worklogExtraQuery =
                "INSERT INTO worklog_fields(worklogid, udf_long1) " +
                "VALUES (@WorklogId, @Kilometers)";

            var worklogExtraParam = new
            {
                WorklogId = worklog.Id,
                Kilometers = worklog.KilometresCovered
            };

            Connection.Execute(worklogExtraQuery, worklogExtraParam, Transaction);

            // Links the worklog with the request
            // Adds extra field values to MSP db
            const string worklogRquestLinkQuery =
                "INSERT INTO workordertocharge(workorderid, chargeid) " +
                "VALUES (@WorklogId, @RequestId)";

            var worklogRequestLinkParam = new
            {
                WorklogId = worklog.Id,
                RequestId = requestId
            };

            Connection.Execute(worklogRquestLinkQuery, worklogRequestLinkParam, Transaction);
        }

        private void AddProjectCode(MspProject project)
        {
            // Creates the project code in MSP
            const string query = "INSERT INTO udf_picklistvalues(picklistid, tablename, columname, value) " +
                                 "VALUES (nextval('picklist_id_seq'), WorkOrder_Fields, UDF_CHAR1, @ProjectName)";

            var param = new
            {
                ProjectName = project.Code
            };

            Connection.Execute(query, param, Transaction);
        }

        public void Remove(MspProject project)
        {
            throw new NotImplementedException();
        }
    }
}