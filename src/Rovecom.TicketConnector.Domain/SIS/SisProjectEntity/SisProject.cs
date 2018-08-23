using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;
using Rovecom.TicketConnector.Domain.SIS.SisWorklogEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using Rovecom.TicketConnector.Domain.Entities;

namespace Rovecom.TicketConnector.Domain.SIS.SisProjectEntity
{
    /// <summary>
    /// A project entity in SIS
    /// </summary>
    public class SisProject
    {
        /// <summary>
        /// Gets or sets the project code
        /// </summary>
        public string Code { get; set; }

        private readonly List<SisWorklog> _worklogs;

        /// <summary>
        /// Default constructor
        /// </summary>
        public SisProject()
        {
            _worklogs = new List<SisWorklog>();
        }

        /// <summary>
        /// Gets or sets the account id
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Removes a sis worklog from the project
        /// </summary>
        /// <param name="worklog">The worklog to be removed</param>
        public void RemoveWorklog(SisWorklog worklog)
        {
            _worklogs.Remove(worklog);
        }

        /// <summary>
        /// Read only collection of worklogs
        /// </summary>
        public IReadOnlyCollection<SisWorklog> Worklogs => _worklogs;

        /// <summary>
        /// Adds a worklog to the project
        /// </summary>
        /// <param name="workStartedDateTime">Date and time work started</param>
        /// <param name="workEndedDateTime">Date and time work ended</param>
        /// <param name="description">Description of the work done</param>
        /// <param name="kilometersCovered">Distance traveled for the work</param>
        /// <param name="employeeId">Id of the employee that did the work</param>
        public void AddWorklog(DateTime workStartedDateTime, DateTime workEndedDateTime, string description, double kilometersCovered,
            int employeeId)
        {
            var wl = new SisWorklog(workStartedDateTime, workEndedDateTime, description, kilometersCovered, employeeId);
            _worklogs.Add(wl);
        }
    }
}