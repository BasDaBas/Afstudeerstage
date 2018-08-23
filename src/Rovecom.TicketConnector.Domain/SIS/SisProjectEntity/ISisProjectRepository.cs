using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Domain.SIS.SisProjectEntity
{
    /// <summary>
    /// The SIS Project repository
    /// </summary>
    public interface ISisProjectRepository
    {
        /// <summary>
        /// Gets all ICTS projects in SIS
        /// </summary>
        Task<IEnumerable<SisProject>> GetAllAsync();

        /// <summary>
        /// Updates a project in SIS
        /// </summary>
        void Update(IProject project);
    }
}