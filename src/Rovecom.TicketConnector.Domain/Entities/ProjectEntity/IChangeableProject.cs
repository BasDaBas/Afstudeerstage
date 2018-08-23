using System.Collections.Generic;

namespace Rovecom.TicketConnector.Domain.Entities.ProjectEntity
{
    /// <inheritdoc />
    /// <summary>
    /// A project wrapper that adds change functionality
    /// </summary>
    public interface IChangeableProject : IProject
    {
        /// <summary>
        /// Adds a change to the project
        /// </summary>
        /// <param name="changes">A project change</param>
        void AddChanges(IEnumerable<IChange> changes);
        
        /// <summary>
        /// Checks if a similar project has conflicting changes with a given collection of changes
        /// </summary>
        /// <param name="changes">Collection of changes</param>
        /// <returns>A collection of conflicting changes</returns>
        IEnumerable<IChange> GetConflictingChangesWith(IEnumerable<IChange> changes);

        /// <summary>
        /// Reverts all changes made to the project
        /// </summary>
        void ClearChanges();
        
        /// <summary>
        /// Applies all project changes
        /// </summary>
        void ApplyChanges();
    }
}