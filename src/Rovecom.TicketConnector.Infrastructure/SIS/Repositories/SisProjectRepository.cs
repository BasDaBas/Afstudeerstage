using Newtonsoft.Json;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.SIS.SisProjectEntity;
using Rovecom.TicketConnector.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Infrastructure.SIS.Repositories
{
    /// <inheritdoc />
    public class SisProjectRepository : ISisProjectRepository
    {
        private readonly ISisApiClient _sisApiClient;
        private const string ProjectTypeCode = "9";
        private const string WorklogTypeCode = "30";

        /// <summary>
        /// Default constructor for the SIS project repository
        /// </summary>
        public SisProjectRepository(ISisApiClient sisApiClient)
        {
            _sisApiClient = sisApiClient;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SisProject>> GetAllAsync()
        {
            var requestEndpoint = $"List/{ProjectTypeCode}/";
            const string parameters = "&LIMITCOUNT=0&LIMITFROM=-1&VIEW=LIJST_PROJECTEN";
            const string filter = "PRODUKTMARKT_ID = 7";
            var response = await _sisApiClient.GetAsync(requestEndpoint, parameters, filter);
            var projectList = new List<SisProject>();
            if (response.IsSuccessStatusCode)
            {
                projectList = await response.Content.ReadAsJsonAsync<List<SisProject>>();
            }
            return projectList;
        }

        /// <inheritdoc />
        public void Update(IProject project)
        {
            var requestEndpoint = $"Entity/{WorklogTypeCode}/0/{WorklogTypeCode}";

            foreach (var projectWorklog in project.Worklogs)
            {
                _sisApiClient.PostAsync(requestEndpoint, new StringContent(JsonConvert.SerializeObject(projectWorklog)));
            }
        }
    }
}