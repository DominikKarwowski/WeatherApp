using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services
{
    public interface IFavouritiesService
    {
        Task<string> LoadFavouriteCity();
        Task SaveFavouriteCity(string cityName);
    }
}