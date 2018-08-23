using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;
using System;
using System.Collections.Generic;

namespace Rovecom.TicketConnector.Domain.Entities.ProjectEntity
{
    /// <inheritdoc />
    /// <summary>
    /// Project Interface
    /// </summary>
    public interface IProject : IEntity
    {
        /// <summary>
        /// Gets or sets the project code
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// Gets or sets the list of worklogs
        /// </summary>
        IReadOnlyCollection<IWorklog> Worklogs { get; }

        /// <summary>
        /// Adds a worklog to the worklog collection
        /// </summary>
        /// <param name="workStartedDateTime">Moment work started</param>
        /// <param name="workEndedDateTime">Moment work added</param>
        /// <param name="description">Description of the work</param>
        /// <param name="kilometersCovered">Distance traveled for the work</param>
        /// <param name="employeeEmail">EmailAddress address of the employee that did the work</param>
        void AddWorklog(DateTime workStartedDateTime, DateTime workEndedDateTime, string description, double kilometersCovered, string employeeEmail);

        /// <summary>
        /// Removes a worklog from the worklog collection
        /// </summary>
        /// <param name="worklog">The worklog to be removed</param>
        void RemoveWorklog(IWorklog worklog);

        /// <summary>
        /// Gets the account code
        /// </summary>
        string AccountCode { get; }
    }
}