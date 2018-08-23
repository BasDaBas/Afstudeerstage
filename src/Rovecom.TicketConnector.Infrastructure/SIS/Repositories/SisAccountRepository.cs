using Rovecom.TicketConnector.Domain.SIS.SisAccountEntity;
using Rovecom.TicketConnector.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Rovecom.TicketConnector.Infrastructure.SIS.Repositories
{
    /// <inheritdoc />
    public class SisAccountRepository : ISisAccountRepository
    {
        private readonly ISisApiClient _sisApiClient;
        private const string SisObjectTypeCode = "4";

        public SisAccountRepository(ISisApiClient sisApiClient)
        {
            _sisApiClient = sisApiClient;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<SisAccount>> GetAllAsync()
        {
            var requestEndpoint = $"List/{SisObjectTypeCode}/";
            const string parameters = "&LIMITCOUNT=0&LIMITFROM=-1&VIEW=LIJST_BEDRIJVEN";
            //const string filter = "PRODUKTMARKT_ID = 7 AND KLANT = true";
            //var filterQuery = HttpUtility.ParseQueryString(filter);
            var streamTask = _sisApiClient.GetStreamAsync(requestEndpoint, parameters);

            var serializer = new DataContractJsonSerializer(typeof(SisApiAccountListResult));
            var accountResult = serializer.ReadObject(await streamTask) as SisApiAccountListResult;
            
            return accountResult?.Result.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<SisAccount> GetByIdAsync(int id)
        {
            var requestEndpoint = $"List/{SisObjectTypeCode}/";
            const string parameters = "&LIMITCOUNT=0&LIMITFROM=-1&VIEW=LIJST_BEDRIJVEN";
            var filter = $"ID={id} AND PRODUKTMARKT_ID = 7 AND KLANT = true";
            var response = await _sisApiClient.GetAsync(requestEndpoint, parameters, filter);
            var accountList = new List<SisAccount>();
            if (response.IsSuccessStatusCode)
            {
                accountList = await response.Content.ReadAsJsonAsync<List<SisAccount>>();
            }

            return accountList.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<SisAccount> GetByCodeAsync(string code)
        {
            var requestEndpoint = $"List/{SisObjectTypeCode}/";
            const string parameters = "&LIMITCOUNT=0&LIMITFROM=-1&VIEW=LIJST_BEDRIJVEN";
            var filter = $"Code='{code}' AND PRODUKTMARKT_ID = 7 AND KLANT = true";
            var response = await _sisApiClient.GetAsync(requestEndpoint, parameters, filter);
            var accountList = new List<SisAccount>();
            if (response.IsSuccessStatusCode)
            {
                accountList = await response.Content.ReadAsJsonAsync<List<SisAccount>>();
            }

            return accountList.FirstOrDefault();
        }
    }
}