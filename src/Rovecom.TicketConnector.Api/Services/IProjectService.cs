using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using System.Collections.Generic;

namespace Rovecom.TicketConnector.Api.Services
{
    /// <summary>
    /// A service class for projects
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Gets all projects with change functionality
        /// </summary>
        /// <returns>Projects with change functionality</returns>
        List<IChangeableProject> GetProjectsAsChangeable(IEnumerable<IProject> projects);

        /// <summary>
        /// Generates changes on the project based on a snapshot of a project collection
        /// </summary>
        /// <param name="targetProjects">Project collection to add the changes to</param>
        /// <param name="newComparableProjects">New project collection</param>
        /// <param name="oldComparableProjects">Old project collection</param>
        /// <returns>Collection of changed projects</returns>
        IEnumerable<IChangeableProject> GenerateChanges(
            IEnumerable<IChangeableProject> targetProjects,
            IEnumerable<IChangeableProject> newComparableProjects,
            IEnumerable<IProject> oldComparableProjects
        );

        /// <summary>
        /// Applies changes to a project collection
        /// </summary>
        /// <param name="changedProjects">Projects to apply changes to</param>
        void ApplyChanges(IEnumerable<IChangeableProject> changedProjects);

        /// <summary>
        /// Copies a project
        /// </summary>
        /// <param name="project">Project to be copied</param>
        /// <returns>Copy of a project entity</returns>
        Project Copy(IProject project);
    }
}