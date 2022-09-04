using DjK.WeatherApp.Core.Services.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services
{
    public class RestService : IRestService
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
    }
}
