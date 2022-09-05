using DjK.WeatherApp.Core.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Repository
{
    public class CityRepository : ICityRepository
    {
        public Task SaveFavouriteCity(string cityName)
        {
            throw new NotImplementedException();
        }
    }
}
