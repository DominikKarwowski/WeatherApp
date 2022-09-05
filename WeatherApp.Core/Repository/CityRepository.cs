using DjK.WeatherApp.Core.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
                    return new Task<string>(() =>
                        File.ReadAllText(_favouriteCityPath));
                }
                else
                {
                    return new Task<string>(() => string.Empty);
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
                return new Task(() =>
                    File.WriteAllText(cityName, _favouriteCityPath));
            }
            catch (Exception)
            {
                // TODO: add logging
                throw;
            }
        }
    }
}
