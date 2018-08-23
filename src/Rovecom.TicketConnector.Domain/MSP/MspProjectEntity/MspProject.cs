using Rovecom.TicketConnector.Domain.MSP.MspAccountEntity;
using Rovecom.TicketConnector.Domain.MSP.MspRequestEntity;
using Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity;
using System.Collections.Generic;
using System.Linq;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;

namespace Rovecom.TicketConnector.Domain.MSP.MspProjectEntity
{
    /// <summary>
    /// A project in MSP
    /// </summary>
    public class MspProject
    {
        private List<MspRequest> _mspRequests;

        /// <summary>
        /// Default constructor
        /// </summary>
        public MspProject()
        {
            _mspRequests = new List<MspRequest>();
        }
        
        /// <summary>
        /// Constructor for creating a project with an identiy
        /// </summary>
        /// <param name="accountId">Id of the account</param>
        /// <param name="code">Code of the account</param>
        public MspProject(long accountId, string code)
        {
            AccountId = accountId;
            Code = code;
            _mspRequests = new List<MspRequest>();
        }

        /// <summary>
        /// Gets or sets the account id
        /// </summary>
        public long AccountId { get; set; }
        
        /// <summary>
        /// Get or sets the project code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The requests that are part of the project
        /// </summary>
        public IReadOnlyCollection<MspRequest> Requests
        {
            get => _mspRequests;
            set => _mspRequests = value.ToList();
        }

        /// <summary>
        /// Adds a request to the project
        /// </summary>
        /// <param name="request"></param>
        public void AddRequest(MspRequest request)
        {
            _mspRequests.Add(request);
        }

        /// <summary>
        /// Adds a worklog to the project
        /// </summary>
        /// <param name="worklog">The added worklog</param>
        /// <param name="request">The request the worklog will be added to</param>
        public void AddWorklog(MspWorklog worklog, MspRequest request)
        {
            // // Get or create default request in MSP to hold worklogs from SIS when no request was passed
            if (request == null)
            {
                if (_mspRequests.Any(mspRequest => mspRequest.Worklogs.Contains(worklog, new MspWorklogComparer())))
                    return;

                request = _mspRequests.FirstOrDefault(r => string.Equals(r.Title, "SIS Werklogs")) ?? new MspRequest("SIS Werklogs", "Default request for SIS worklogs", 0);
                _mspRequests.Add(request);
            }

            if (!_mspRequests.Contains(request))
                return;

            request.AddWorklog(worklog);
        }

        /// <summary>
        /// Removes the worklog from the project
        /// </summary>
        /// <param name="worklog">The removed worklog</param>
        /// <param name="request">The request the worklog will be removed from</param>
        public void RemoveWorklog(MspWorklog worklog, MspRequest request)
        {
            if (!_mspRequests.Contains(request))
                return;

            request?.RemoveWorklog(worklog);
        }
    }
}