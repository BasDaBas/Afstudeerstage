using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Infrastructure.SIS
{
    /// <summary>
    /// Service for communicating with the SIS API
    /// </summary>
    public interface ISisApiClient
    {
        /// <summary>
        /// Sends a GET request to the SIS API as an asynchronous operation
        /// </summary>
        /// <param name="uri">URI request is send to</param>
        /// <param name="parameters">Parameters for the request</param>
        /// <param name="filter">Filters for the request</param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAsync(string uri, string parameters, string filter = null);

        /// <summary>
        /// Sends a GET request to the SIS API as an asynchronous operation
        /// </summary>
        /// <param name="uri">URI request is send to</param>
        /// <param name="parameters">Parameters for the request</param>
        /// <param name="filter">Filters for the request</param>
        /// <returns></returns>
        Task<Stream> GetStreamAsync(string uri, string parameters, string filter = null);

        /// <summary>
        /// Sends a PUT request to the SIS API as an asynchronous operation
        /// </summary>
        /// <param name="uri">URI request is send to</param>
        /// <param name="content">The HTTP request content sent to server</param>
        /// <returns></returns>
        Task<HttpResponseMessage> PutAsync(string uri, HttpContent content);

        /// <summary>
        /// Sends a POST request to the SIS API as an asynchronous operation
        /// </summary>
        /// <param name="uri">URI request is send to</param>
        /// <param name="content">The HTTP request content sent to server</param>
        /// <returns></returns>
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent content);
    }
}