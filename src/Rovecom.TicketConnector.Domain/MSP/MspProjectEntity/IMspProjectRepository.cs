using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using System.Collections.Generic;

namespace Rovecom.TicketConnector.Domain.MSP.MspProjectEntity
{
    /// <summary>
    /// Repository class for MSP projects
    /// </summary>
    public interface IMspProjectRepository
    {
        /// <summary>
        /// Gets all MSP projects
        /// </summary>
        /// <returns></returns>
        IEnumerable<MspProject> GetAll();

        /// <summary>
        /// Adds a project to MSP
        /// </summary>
        /// <param name="project"></param>
        void Add(MspProject project);

        /// <summary>
        /// Updates a project in MSP
        /// </summary>
        /// <param name="project">The updated project</param>
        void Update(IProject project);

        /// <summary>
        /// Removes a project in MSP
        /// </summary>
        /// <param name="project">The removed project</param>
        void Remove(MspProject project);
    }
}