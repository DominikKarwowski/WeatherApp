using DjK.WeatherApp.Core.Models;
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
        /// <param name="cityName">City name for which weather data are requested.</param>
        /// <param name="language">Language in which weather data are requested.</param>
        /// <param name="isMetric">Requested units for a weather data.</param>
        /// <returns>Weather data along with information about the request status.</returns>
        Task<WeatherResponse> GetWeatherResponse(WeatherRequestParameters parameters);
    }
}
