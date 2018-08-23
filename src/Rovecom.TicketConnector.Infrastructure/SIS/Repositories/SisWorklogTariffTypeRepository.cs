using Rovecom.TicketConnector.Domain.SIS.SisWorklogEntity;
using Rovecom.TicketConnector.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Infrastructure.SIS.Repositories
{
    /// <inheritdoc />
    public class SisWorklogTariffTypeRepository : ISisWorklogTariffTypeRepository
    {
        private readonly ISisApiClient _sisApiClient;
        private const string SisObjectTypeCode = "155";

        /// <summary>
        /// Default constructor for SIS worklog tariff type repository
        /// </summary>
        /// <param name="sisApiClient"><see cref="ISisApiClient"/></param>
        public SisWorklogTariffTypeRepository(ISisApiClient sisApiClient)
        {
            _sisApiClient = sisApiClient;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<SisWorklogTariffType>> GetAllAsync()
        {
            var requestEndpoint = $"List/{SisObjectTypeCode}/";
            const string parameters = "&LIMITCOUNT=0&LIMITFROM=-1&VIEW=LIJST_TARIEVEN";
            var response = await _sisApiClient.GetAsync(requestEndpoint, parameters);
            var worklogTariffTypeList = new List<SisWorklogTariffType>();
            if (response.IsSuccessStatusCode)
            {
                worklogTariffTypeList = await response.Content.ReadAsJsonAsync<List<SisWorklogTariffType>>();
            }

            return worklogTariffTypeList;
        }
    }
}