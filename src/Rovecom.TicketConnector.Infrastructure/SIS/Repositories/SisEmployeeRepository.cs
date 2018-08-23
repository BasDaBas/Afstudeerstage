using Rovecom.TicketConnector.Domain.SIS.SisEmployeeEntity;
using Rovecom.TicketConnector.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Infrastructure.SIS.Repositories
{
    /// <inheritdoc />
    public class SisEmployeeRepository : ISisEmployeeRepository
    {
        private readonly ISisApiClient _sisApiClient;
        private const string SisObjectTypeCode = "8";

        /// <summary>
        /// Defautl constructors for SIS API client
        /// </summary>
        /// <param name="sisApiClient"></param>
        public SisEmployeeRepository(ISisApiClient sisApiClient)
        {
            _sisApiClient = sisApiClient;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SisEmployee>> GetAllASync()
        {
            var requestEndpoint = $"List/{SisObjectTypeCode}/";
            const string parameters = "&LIMITCOUNT=0&LIMITFROM=-1&VIEW=LIJST_WERKNEMERS_EMAIL";
            var response = await _sisApiClient.GetAsync(requestEndpoint, parameters);
            var employeeList = new List<SisEmployee>();
            if (response.IsSuccessStatusCode)
            {
                employeeList = await response.Content.ReadAsJsonAsync<List<SisEmployee>>();
            }

            return employeeList;
        }

        /// <inheritdoc />
        public async Task<SisEmployee> GetByEmailAsync(string emailAddress)
        {
            var requestEndpoint = $"List/{SisObjectTypeCode}/";
            var filter = $"EMAIL={emailAddress}";
            const string parameters = "&LIMITCOUNT=0&LIMITFROM=-1&VIEW=LIJST_WERKNEMERS_EMAIL";
            var response = await _sisApiClient.GetAsync(requestEndpoint, parameters, filter);
            var employeeList = new List<SisEmployee>();
            if (response.IsSuccessStatusCode)
            {
                employeeList = await response.Content.ReadAsJsonAsync<List<SisEmployee>>();
            }

            return employeeList.FirstOrDefault();
        }
    }
}