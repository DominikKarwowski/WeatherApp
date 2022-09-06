using DjK.WeatherApp.Core.Repository.Abstractions;
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
        private readonly string _favouriteCityPath;

        /// <summary>
        /// Creates a CityRepository instance.
        /// </summary>
        public CityRepository()
        {
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
            catch (Exception)
            {
                // TODO: add logging
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
            catch (Exception)
            {
                // TODO: add logging
                throw;
            }
        }
    }
}
