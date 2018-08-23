using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;

namespace Rovecom.TicketConnector.Domain.Entities.WorklogChangeEntity
{
    /// <summary>
    /// Factory class for worklog changes
    /// </summary>
    public interface IWorklogChangeFactory
    {
        /// <summary>
        /// Returns a worklog removed change
        /// </summary>
        /// <param name="worklog">The removed worklog</param>
        /// <returns>A worklog change</returns>
        IWorklogChange GetWorklogRemovedChange(IWorklog worklog);

        /// <summary>
        /// Returns a worklog added change
        /// </summary>
        /// <param name="worklog">The added worklog</param>
        /// <returns>A worklog change</returns>
        IWorklogChange GetWorklogAddedChange(IWorklog worklog);
    }
}