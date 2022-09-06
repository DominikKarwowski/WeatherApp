using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services
{
    /// <summary>
    /// Represents a service layer for storing favourities.
    /// </summary>
    public interface IFavouritiesService
    {
        /// <summary>
        /// Represents a method to save a favourite city.
        /// </summary>
        /// <param name="cityName">City name to be saved to a storage.</param>
        /// <returns></returns>
        Task SaveFavouriteCity(string cityName);

        /// <summary>
        /// Represents a method to load favourite city.
        /// </summary>
        /// <returns></returns>
        Task<string> LoadFavouriteCity();

    }
}