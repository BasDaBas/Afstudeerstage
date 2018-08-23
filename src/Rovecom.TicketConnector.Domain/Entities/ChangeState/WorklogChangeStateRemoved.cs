using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.Entities.WorklogChangeEntity;
using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;
using System;
using System.Linq;

namespace Rovecom.TicketConnector.Domain.Entities.ChangeState
{
    /// <inheritdoc />
    /// <summary>
    /// The state of a worklog that is removed
    /// </summary>
    public class WorklogChangeStateRemoved : IWorklogChangeState
    {
        /// <inheritdoc />
        public void Apply(IProject project, IWorklog worklog)
        {
            var removedWorklog = project.Worklogs.FirstOrDefault(x =>
                x.WorkEndedDateTime == worklog.WorkEndedDateTime &&
                x.WorkStartedDateTime == worklog.WorkStartedDateTime &&
                string.Equals(x.EmployeeEmailAddress, worklog.EmployeeEmailAddress) &&
                Math.Abs(x.KilometresCovered - worklog.KilometresCovered) < 0.01);

            if (removedWorklog != null)
                project.RemoveWorklog(worklog);
        }

        /// <inheritdoc />
        public bool IsConflicting(WorklogChange originalChange, WorklogChange similarChange)
        {
            var wlEqC = new WorklogEqualityComparer();
            var conflict = originalChange.WorklogChangeState.GetType() != similarChange.WorklogChangeState.GetType() &&
                           wlEqC.Equals(originalChange.Worklog, similarChange.Worklog);
            return conflict;
        }
    }
}