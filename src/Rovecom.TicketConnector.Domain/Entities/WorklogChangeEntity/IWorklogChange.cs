using Rovecom.TicketConnector.Domain.Entities.ChangeState;
using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;

namespace Rovecom.TicketConnector.Domain.Entities.WorklogChangeEntity
{
    /// <inheritdoc />
    /// <summary>
    /// A worklog change in a project
    /// </summary>
    public interface IWorklogChange : IChange
    {
        /// <summary>
        /// The changed workklog
        /// </summary>
        IWorklog Worklog { get; }

        /// <summary>
        /// The state the changed worklog is in
        /// </summary>
        IWorklogChangeState WorklogChangeState { get; }
    }
}