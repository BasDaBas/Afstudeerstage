using Rovecom.TicketConnector.Domain.Entities.ChangeState;
using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;

namespace Rovecom.TicketConnector.Domain.Entities.WorklogChangeEntity
{
    /// <inheritdoc />
    public class WorklogChangeFactory : IWorklogChangeFactory
    {
        /// <inheritdoc />
        public IWorklogChange GetWorklogRemovedChange(IWorklog worklog)
        {
            return new WorklogChange(worklog, new WorklogChangeStateRemoved());
        }

        /// <inheritdoc />
        public IWorklogChange GetWorklogAddedChange(IWorklog worklog)
        {
            return new WorklogChange(worklog, new WorklogChangeStateAdded());
        }
    }
}