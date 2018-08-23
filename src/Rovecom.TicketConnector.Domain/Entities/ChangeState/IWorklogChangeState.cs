using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.Entities.WorklogChangeEntity;
using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;

namespace Rovecom.TicketConnector.Domain.Entities.ChangeState
{
    /// <summary>
    /// Represents the state of change the worklog is in
    /// </summary>
    public interface IWorklogChangeState
    {
        /// <summary>
        /// Applies a change to the project
        /// </summary>
        /// <param name="project">The target project</param>
        /// <param name="worklog">The worklog that is changed</param>
        void Apply(IProject project, IWorklog worklog);

        /// <summary>
        /// Checks if two changes are conflicting each other
        /// </summary>
        /// <param name="originalChange">The original change</param>
        /// <param name="similarChange">The change to compare with</param>
        /// <returns></returns>
        bool IsConflicting(WorklogChange originalChange, WorklogChange similarChange);
    }
}