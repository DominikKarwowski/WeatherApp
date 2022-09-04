using DjK.WeatherApp.Core.Models;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services.Abstractions
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherResponseForLocation(string cityName);
    }
}
