using DjK.WeatherApp.Core.Repository.Abstractions;
using System;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services
{
    public class FavouritiesService : IFavouritiesService
    {
        private readonly ICityRepository _cityRepository;

        public FavouritiesService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
        }

        public async Task SaveFavouriteCity(string cityName)
        {
            await _cityRepository
                .SaveFavouriteCity(cityName)
                .ConfigureAwait(false);
        }

        public async Task<string> LoadFavouriteCity()
        {
            return await _cityRepository
                .LoadFavouriteCity()
                .ConfigureAwait(false);
        }

    }
}
