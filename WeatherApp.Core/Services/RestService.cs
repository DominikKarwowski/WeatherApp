using DjK.WeatherApp.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services
{
    /// <summary>
    /// Implementation of the Rest service.
    /// </summary>
    public class RestService : IRestService, IDisposable
    {
        private readonly ILogger<RestService> _logger;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Creates RestService instance.
        /// <param name="logger">Logger implementation.</param>
        /// </summary>
        public RestService(ILogger<RestService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Retrieves response from a Web request.
        /// </summary>
        /// <param name="requestUri">Request endpoint.</param>
        /// <returns>Response message obtained from a Web service.</returns>
        public Task<HttpResponseMessage> GetHttpResponseMessage(string requestUri)
        {
            try
            {
                return _httpClient.GetAsync(requestUri);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

    }
}
