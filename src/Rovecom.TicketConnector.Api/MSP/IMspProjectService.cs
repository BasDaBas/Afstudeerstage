using System.Collections.Generic;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.MSP.MspProjectEntity;

namespace Rovecom.TicketConnector.Api.MSP
{
    /// <summary>
    /// Service for MSP projects
    /// </summary>
    public interface IMspProjectService
    {
        /// <summary>
        /// Gets all projects in MSP
        /// </summary>
        /// <returns>Collection of MSP projects</returns>
        IEnumerable<MspProject> GetAll();

        /// <summary>
        /// Gets all projects in MSP as adapted
        /// </summary>
        /// <returns>Collection of adapted MSP projects</returns>
        IEnumerable<IProject> GetProjectsAsAdapted();

        /// <summary>
        /// Persists the changes of the projects
        /// </summary>
        /// <param name="updatedMspProjects">The updated projects</param>
        void UpdateProjects(IEnumerable<IProject> updatedMspProjects);

        /// <summary>
        /// Creates new MSP projects from given projects
        /// </summary>
        /// <param name="newProjects">New projects</param>
        void CreateProjects(IEnumerable<IProject> newProjects);
    }
}