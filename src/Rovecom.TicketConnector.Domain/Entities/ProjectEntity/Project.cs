using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Rovecom.TicketConnector.Domain.Entities.AccountEntity;

namespace Rovecom.TicketConnector.Domain.Entities.ProjectEntity
{
    /// <inheritdoc cref="IProject" />
    public class Project : Entity, IProject
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="code">Code of the project</param>
        /// <param name="accountCode">Code of the account the project belongs to</param>
        public Project(string code, string accountCode)
        {
            Code = code;
            AccountCode = accountCode;
            ConnectorWorklogs = new List<Worklog>();
        }

        /// <inheritdoc />
        public void AddWorklog(DateTime workStartedDateTime, DateTime workEndedDateTime, string description, double kilometersCovered,
            string employeeEmail)
        {
            // Project cannot have two worklogs where work was done during the same time by the same employee
            if (ConnectorWorklogs.Any(x => x.WorkStartedDateTime == workStartedDateTime &&
                                           x.WorkEndedDateTime == workEndedDateTime &&
                                           string.Equals(x.EmployeeEmailAddress, employeeEmail)))
            {
                throw new DomainException("Similar worklog already exists in project");
            }

            ConnectorWorklogs.Add(new Worklog(workStartedDateTime, workEndedDateTime, description, kilometersCovered, employeeEmail));
        }

        /// <inheritdoc />
        public void RemoveWorklog(IWorklog worklog)
        {
            var removedWorklog = ConnectorWorklogs.FirstOrDefault(x =>
                x.WorkStartedDateTime == worklog.WorkStartedDateTime &&
                x.WorkEndedDateTime == worklog.WorkEndedDateTime &&
                Math.Abs(x.KilometresCovered - worklog.KilometresCovered) < 0.01 &&
                string.Equals(x.EmployeeEmailAddress, worklog.EmployeeEmailAddress));

            if (removedWorklog == null)
                throw new DomainException("Worklog does not exist in project");

            ConnectorWorklogs.Remove(removedWorklog);
        }

        /// <inheritdoc />
        public string AccountCode { get; set; }

        public long Id { get; set; }

        public Account Account { get; set; }

        /// <inheritdoc />
        public string Code { get; set; }

        /// <inheritdoc />
        [NotMapped]
        public IReadOnlyCollection<IWorklog> Worklogs => ConnectorWorklogs;

        /// <summary>
        /// Gets or sets the worklogs
        /// </summary>
        public List<Worklog> ConnectorWorklogs { get; set; }
    }
}