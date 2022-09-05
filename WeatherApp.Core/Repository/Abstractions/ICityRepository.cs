using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Repository.Abstractions
{
    public interface ICityRepository
    {
        Task SaveFavouriteCity(string cityName);
    }
}
