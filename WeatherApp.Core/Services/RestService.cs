using DjK.WeatherApp.Core.Services.Abstractions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services
{
    public class RestService : IRestService, IDisposable
    {
        private readonly HttpClient _httpClient;

        public RestService()
        {
            _httpClient = new HttpClient();
        }

        public Task<HttpResponseMessage> GetHttpResponseMessage(string requestUri)
        {
            return _httpClient.GetAsync(requestUri);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

    }
}
