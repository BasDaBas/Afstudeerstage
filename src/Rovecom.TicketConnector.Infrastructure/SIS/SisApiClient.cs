using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Infrastructure.SIS
{
    /// <inheritdoc />
    public class SisApiClient : ISisApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly SisApiConfig _sisApiConfig;

        /// <summary>
        /// Default constructor for the SIS API client
        /// </summary>
        /// <param name="httpClient"><see cref="HttpClient"/></param>
        /// <param name="sisApiConfigAccessor">Configuration options for the SIS API Client</param>
        public SisApiClient(HttpClient httpClient, IOptions<SisApiConfig> sisApiConfigAccessor)
        {
            _sisApiConfig = sisApiConfigAccessor.Value;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var userName = $"{_sisApiConfig.AppCode}{_sisApiConfig.UserName}";
            var passWordHash = Hash(_sisApiConfig.Password, _sisApiConfig.PasswordHashKey);
            var encodedPasswordHash = Convert.ToBase64String(passWordHash);
            var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{encodedPasswordHash}")));
            httpClient.DefaultRequestHeaders.Authorization = authValue;

            _httpClient = httpClient;
        }

        /// <inheritdoc />
        public Task<HttpResponseMessage> GetAsync(string uri, string parameters, string filter = null)
        {
            var baseUri = AddBaseValues(uri);
            var fullUri = baseUri + parameters;
            if (!string.IsNullOrEmpty(filter))
                fullUri = $"{fullUri}&MANUALFILTER={filter}";

            return _httpClient.GetAsync(fullUri);
        }

        /// <inheritdoc />
        public Task<Stream> GetStreamAsync(string uri, string parameters, string filter = null)
        {
            var baseUri = AddBaseValues(uri);
            var fullUri = baseUri + parameters;
            if (!string.IsNullOrEmpty(filter))
                fullUri = $"{fullUri}&MANUALFILTER={filter}";

            return _httpClient.GetStreamAsync(fullUri);
        }

        /// <inheritdoc />
        public Task<HttpResponseMessage> PutAsync(string uri, HttpContent content)
        {
            var fullUri = AddBaseValues(uri);
            return _httpClient.PutAsync(fullUri, content);
        }

        /// <inheritdoc />
        public Task<HttpResponseMessage> PostAsync(string uri, HttpContent content)
        {
            var fullUri = AddBaseValues(uri);
            return _httpClient.PutAsync(fullUri, content);
        }

        /// Adds base values to the URI so the SIS API will accept it
        private string AddBaseValues(string uri)
        {
            var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var signature = $"{timeStamp}{_sisApiConfig.UserName}{_sisApiConfig.AppCode}";
            var signatureHash = Hash(signature, _sisApiConfig.SignatureHashKey);
            var encodedSignatureHash = Convert.ToBase64String(signatureHash);
            var rand = new Random();
            return $"{_sisApiConfig.Url}/{uri}?timestamp={timeStamp}&signature={encodedSignatureHash}&taskid=&prio=1";
        }

        // Returns a hash byte array created from the input string and key in a GUID format
        private static byte[] Hash(string input, string key)
        {
            var keyByteArray = Encoding.ASCII.GetBytes(key);
            var hmacSha1 = new HMACSHA1(keyByteArray);

            hmacSha1.Initialize();

            var byteArray = Encoding.ASCII.GetBytes(input);
            return hmacSha1.ComputeHash(byteArray);
        }
    }
}