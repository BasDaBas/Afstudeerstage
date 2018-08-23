using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.Entities.WorklogChangeEntity;
using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;

namespace Rovecom.TicketConnector.Domain.Entities.ChangeState
{
    /// <inheritdoc />
    /// <summary>
    /// The state of a worklog that was added
    /// </summary>
    public class WorklogChangeStateAdded : IWorklogChangeState
    {
        /// <inheritdoc />
        public void Apply(IProject project, IWorklog worklog)
        {
            project.AddWorklog(worklog.WorkStartedDateTime, worklog.WorkEndedDateTime, worklog.Description, worklog.KilometresCovered, worklog.EmployeeEmailAddress);
        }

        /// <inheritdoc />
        public bool IsConflicting(WorklogChange originalChange, WorklogChange similarChange)
        {
            var wlEqC = new WorklogEqualityComparer();
            return originalChange.WorklogChangeState.GetType() != similarChange.WorklogChangeState.GetType() &&
                   wlEqC.Equals(originalChange.Worklog, similarChange.Worklog);
        }
    }
}