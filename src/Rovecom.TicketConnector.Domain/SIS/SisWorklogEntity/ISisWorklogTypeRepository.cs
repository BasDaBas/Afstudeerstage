using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Domain.SIS.SisWorklogEntity
{
    /// <summary>
    /// Repository service for worklog types
    /// </summary>
    public interface ISisWorklogTypeRepository
    {
        /// <summary>
        /// Gets all worklog types from SIS
        /// </summary>
        Task<IReadOnlyCollection<SisWorklogType>> GetAllAsync();
    }
}
