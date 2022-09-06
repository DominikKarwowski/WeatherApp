using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Repository.Abstractions
{
    /// <summary>
    /// Represents a repository layer for favourite city.
    /// </summary>
    public interface ICityRepository
    {
        /// <summary>
        /// Represents a method to save a favourite city to a storage.
        /// </summary>
        /// <param name="cityName">City name to be saved to a storage.</param>
        /// <returns></returns>
        Task SaveFavouriteCity(string cityName);

        /// <summary>
        /// Represents a method to load favourite city from a storage.
        /// </summary>
        /// <returns>Favourite city name.</returns>
        Task<string> LoadFavouriteCity();
    }
}
