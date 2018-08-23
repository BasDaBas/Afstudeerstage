using System.Collections.Generic;
using System.Threading.Tasks;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.SIS.SisProjectEntity;

namespace Rovecom.TicketConnector.Api.SIS
{
    /// <summary>
    /// Service for SIS projects
    /// </summary>
    public interface ISisProjectService
    {
        /// <summary>
        /// Gets SIS projects
        /// </summary>
        /// <returns>Collection of SIS projects</returns>
        Task<IEnumerable<SisProject>> GetSisProjectsAsync();

        /// <summary>
        /// Gets adapted SIS projects
        /// </summary>
        /// <returns>Collection of adapted SIS projects</returns>
        Task<IEnumerable<IProject>> GetProjectsAsAdaptedAsync();

        /// <summary>
        /// Persists the updated values of the projects
        /// </summary>
        /// <param name="changedSisProjects"></param>
        void UpdateProjects(List<IChangeableProject> changedSisProjects);
    }
}