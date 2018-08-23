using System;
using MediatR;
using Rovecom.TicketConnector.Domain.Entities.WorklogChangeEntity;

namespace Rovecom.TicketConnector.Domain.Events
{
    /// <inheritdoc />
    public class WorklogChangeConflictEvent : INotification
    {
        /// <summary>
        /// Default constructor for the worklog change conflict event
        /// </summary>
        /// <param name="projectCode">Code of the project the worklog belongs to</param>
        /// <param name="worklogChange"><see cref="IWorklogChange"/></param>
        public WorklogChangeConflictEvent(string projectCode, IWorklogChange worklogChange)
        {
            ProjectCode = projectCode;
            WorklogChange = worklogChange;
        }

        /// <summary>
        /// Gets the project code
        /// </summary>
        public string ProjectCode { get; }

        /// <summary>
        /// Gets the worklog change
        /// </summary>
        public IWorklogChange WorklogChange { get; }
    }
}
