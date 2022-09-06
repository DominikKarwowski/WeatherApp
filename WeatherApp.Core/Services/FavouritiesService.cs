using DjK.WeatherApp.Core.Repository.Abstractions;
using System;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services
{
    /// <summary>
    /// Favourities service implementation.
    /// </summary>
    public class FavouritiesService : IFavouritiesService
    {
        private readonly ICityRepository _cityRepository;

        /// <summary>
        /// Creates a FavouritiesService instance.
        /// </summary>
        /// <param name="cityRepository">Implementation of ICityRepository.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public FavouritiesService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
        }

        /// <summary>
        /// Saves a favourite city.
        /// </summary>
        /// <param name="cityName">Name of the city to be saved.</param>
        /// <returns></returns>
        public async Task SaveFavouriteCity(string cityName)
        {
            await _cityRepository
                .SaveFavouriteCity(cityName)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Loads a favourite city.
        /// </summary>
        /// <returns>Name of the saved city.</returns>
        public async Task<string> LoadFavouriteCity()
        {
            return await _cityRepository
                .LoadFavouriteCity()
                .ConfigureAwait(false);
        }

    }
}
