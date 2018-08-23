using MediatR;
using Rovecom.TicketConnector.Domain.Entities.ChangeState;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;
using Rovecom.TicketConnector.Domain.Events;

namespace Rovecom.TicketConnector.Domain.Entities.WorklogChangeEntity
{
    /// <inheritdoc />
    public class WorklogChange : IWorklogChange
    {
        public WorklogChange(IWorklog worklog, IWorklogChangeState worklogChangeState)
        {
            Worklog = worklog;
            WorklogChangeState = worklogChangeState;
        }

        public IWorklog Worklog { get; }
        public IWorklogChangeState WorklogChangeState { get; }

        public void Apply(IProject project)
        {
            WorklogChangeState.Apply(project, Worklog);
        }

        public bool IsConflicting(IChange change)
        {
            if (!(change is WorklogChange castedChange))
                return false;

            return WorklogChangeState.IsConflicting(this, castedChange);
        }

        public INotification CreateConflictingChangeEvent(string projectCode)
        {
            return new WorklogChangeConflictEvent(projectCode, this);
        }
    }
}