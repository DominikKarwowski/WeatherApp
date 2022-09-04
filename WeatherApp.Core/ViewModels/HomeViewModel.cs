using DjK.WeatherApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private string cityName;

        public string CityName
        {
            get { return cityName; }
            set { SetProperty(ref cityName, value); }
        }


        public IMvxAsyncCommand ShowWeatherDetailsCommand => new MvxAsyncCommand(ShowWeatherDetails);


        public HomeViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        private async Task ShowWeatherDetails()
        {
            //TODO: get actual WeatherDetails based on the provided city and replace dummy WeatherDetails
            var weatherDetails = new WeatherDetails()
            {
                CityName = CityName,
                Description = $"dummy weather description for {CityName}",
                Temperature = 100,
                TemperatureFeelsLike = 99,
                TemperatureMax = 103,
                TemperatureMin = 97
            };

            await _navigationService.Navigate(typeof(WeatherDetailsViewModel), weatherDetails);
        }
    }
}
