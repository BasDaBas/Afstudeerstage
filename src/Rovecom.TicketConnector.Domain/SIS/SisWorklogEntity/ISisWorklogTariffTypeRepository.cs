using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Domain.SIS.SisWorklogEntity
{
    /// <summary>
    /// Repository service for worklog tariff types
    /// </summary>
    public interface ISisWorklogTariffTypeRepository
    {
        /// <summary>
        /// Gets all worklog tariff types from SIS
        /// </summary>
        Task<IReadOnlyCollection<SisWorklogTariffType>> GetAllAsync();
    }
}
