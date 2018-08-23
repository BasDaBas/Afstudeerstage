using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;

namespace Rovecom.TicketConnector.Domain.Services
{
    /// <summary>
    /// Domain service for project changes
    /// </summary>
    public interface IProjectChangeService
    {
        /// <summary>
        /// Generates changes on target project
        /// </summary>
        /// <param name="targetProject"></param>
        /// <param name="oldProject"></param>
        /// <param name="newProject"></param>
        bool TryGenerateChanges(IChangeableProject targetProject, IProject oldProject, IChangeableProject newProject);
    }
}
