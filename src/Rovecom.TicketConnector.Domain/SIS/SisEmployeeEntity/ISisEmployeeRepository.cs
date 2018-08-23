using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Domain.SIS.SisEmployeeEntity
{
    public interface ISisEmployeeRepository
    {
        Task<IEnumerable<SisEmployee>> GetAllASync();
        Task<SisEmployee> GetByEmailAsync(string emailAddress);
    }
}
