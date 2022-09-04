using System.Net.Http;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services.Abstractions
{
    public interface IRestService
    {
        Task<HttpResponseMessage> GetHttpResponseMessage(string requestUri);
    }
}
