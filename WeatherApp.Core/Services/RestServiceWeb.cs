using DjK.WeatherApp.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services
{
    /// <summary>
    /// Implementation of the Rest service.
    /// </summary>
    public class RestServiceWeb : IRestServiceWeb, IDisposable
    {
        private readonly ILogger<RestServiceWeb> _logger;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Creates RestService instance.
        /// <param name="logger">Logger implementation.</param>
        /// </summary>
        public RestServiceWeb(ILogger<RestServiceWeb> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Retrieves response from a Web request.
        /// </summary>
        /// <param name="requestUri">Request endpoint.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response message obtained from a Web service.</returns>
        public Task<HttpResponseMessage> GetHttpResponseMessage(
            string requestUri, CancellationToken cancellationToken)
        {
            try
            {
                return _httpClient.GetAsync(requestUri, cancellationToken);
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
