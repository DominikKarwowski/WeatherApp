using DjK.WeatherApp.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services.Abstractions
{
    /// <summary>
    /// Represents a service for preparing and posting a request for weather data.
    /// </summary>
    public interface IWeatherServiceWeb
    {
        /// <summary>
        /// Represents a method to prepare and post request for a weather data.
        /// </summary>
        /// <param name="parameters">Request parameters as described in WeatherRequestParameters object.</param>
        /// <param name="token">Request cancellation token.</param>
        /// <returns>Weather data along with information about the request status.</returns>
        Task<WeatherResponse> GetWeatherResponse(WeatherRequestParameters parameters, CancellationToken token);
    }
}
