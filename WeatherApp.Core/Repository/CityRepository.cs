using DjK.WeatherApp.Core.Repository.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DjK.WeatherApp.Core.Repository
{
    /// <summary>
    /// City repository implementation. Stores favourite city in a local storage.
    /// </summary>
    public class CityRepository : ICityRepository
    {
        private readonly ILogger<CityRepository> _logger;
        private readonly string _favouriteCityPath;

        /// <summary>
        /// Creates a CityRepository instance.
        /// </summary>
        /// <param name="logger">Logger implementation.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CityRepository(ILogger<CityRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _favouriteCityPath = Path.Combine(FileSystem.AppDataDirectory, "favourite_city.txt");
        }

        /// <summary>
        /// Loads favourite city from a storage.
        /// </summary>
        /// <returns>Favourite city name.</returns>
        public Task<string> LoadFavouriteCity()
        {
            try
            {
                if (File.Exists(_favouriteCityPath))
                {
                    return Task.Run(() =>
                        File.ReadAllText(_favouriteCityPath));
                }
                else
                {
                    return Task.Run(() => string.Empty);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Saves a favourite city to a storage.
        /// </summary>
        /// <param name="cityName">City name to be saved to a storage.</param>
        /// <returns></returns>
        public Task SaveFavouriteCity(string cityName)
        {
            try
            {
                return Task.Run(() =>
                    File.WriteAllText(_favouriteCityPath, cityName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
