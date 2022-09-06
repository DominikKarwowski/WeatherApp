using DjK.WeatherApp.Core.Repository.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DjK.WeatherApp.Core.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly string _favouriteCityPath;

        public CityRepository()
        {
            _favouriteCityPath = Path.Combine(FileSystem.AppDataDirectory, "favourite_city.txt");
        }

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
