using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services.Abstractions
{
    /// <summary>
    /// Represents REST service layer.
    /// </summary>
    public interface IRestServiceWeb
    {
        /// <summary>
        /// Represents a method to retrieve response from a Web request.
        /// </summary>
        /// <param name="requestUri">Request endpoint.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Response message obtained from a Web service.</returns>
        Task<HttpResponseMessage> GetHttpResponseMessage(string requestUri, CancellationToken token);
    }
}
