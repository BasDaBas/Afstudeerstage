using Rovecom.TicketConnector.Domain.SIS.SisWorklogEntity;
using Rovecom.TicketConnector.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Infrastructure.SIS.Repositories
{
    /// <inheritdoc />
    public class SisWorklogTypeRepository : ISisWorklogTypeRepository
    {
        private readonly ISisApiClient _sisApiClient;
        private const string SisObjectTypeCode = "155";

        /// <summary>
        /// Default constructor for SIS worklog type repository
        /// </summary>
        /// <param name="sisApiClient"><see cref="ISisApiClient"/></param>
        public SisWorklogTypeRepository(ISisApiClient sisApiClient)
        {
            _sisApiClient = sisApiClient;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<SisWorklogType>> GetAllAsync()
        {
            var requestEndpoint = $"List/{SisObjectTypeCode}/";
            const string parameters = "&LIMITCOUNT=0&LIMITFROM=-1&VIEW=LIJST_UURSOORTEN";
            var response = await _sisApiClient.GetAsync(requestEndpoint, parameters);
            var worklogTypeList = new List<SisWorklogType>();
            if (response.IsSuccessStatusCode)
            {
                worklogTypeList = await response.Content.ReadAsJsonAsync<List<SisWorklogType>>();
            }

            return worklogTypeList;
        }
    }
}