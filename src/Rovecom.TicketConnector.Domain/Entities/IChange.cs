using MediatR;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;

namespace Rovecom.TicketConnector.Domain.Entities
{
    /// <summary>
    /// A change on changeable project
    /// </summary>
    public interface IChange
    {
        /// <summary>
        /// Applies a change to a project
        /// </summary>
        /// <param name="project">The target project</param>
        void Apply(IProject project);

        /// <summary>
        /// Checks if two changes are conflicting each other
        /// </summary>
        /// <param name="change"></param>
        /// <returns>True if the change conflicts</returns>
        bool IsConflicting(IChange change);

        /// <summary>
        /// Creates a conflicting change domain event
        /// </summary>
        /// /// <param name="projectCode">Code of the project the change belongs to</param>
        /// <returns>Conflicting change domain event</returns>
        INotification CreateConflictingChangeEvent(string projectCode);
    }
}