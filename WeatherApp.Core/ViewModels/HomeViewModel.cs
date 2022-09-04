using DjK.WeatherApp.Core.Models;
using DjK.WeatherApp.Core.Services.Abstractions;
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
        private readonly IWeatherService _weatherService;

        private string cityName;
        private string errorMessage;

        public string CityName
        {
            get { return cityName; }
            set { SetProperty(ref cityName, value); }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        public IMvxAsyncCommand ShowWeatherDetailsCommand => new MvxAsyncCommand(ShowWeatherDetails);


        public HomeViewModel(IMvxNavigationService navigationService, IWeatherService weatherService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        }

        private async Task ShowWeatherDetails()
        {
            try
            {
                var weatherResponse = await _weatherService.GetWeatherResponseForLocation(CityName);
                if (weatherResponse.IsSuccessful)
                {
                    ErrorMessage = string.Empty;
                    await _navigationService.Navigate(typeof(WeatherDetailsViewModel), weatherResponse.WeatherDetails);
                }
                else
                {
                    ErrorMessage = weatherResponse.ReasonPhrase;
                }

            }
            catch (Exception)
            {
                // TODO: add logging
                ErrorMessage = "Unexpected exception";
            }
        }
    }
}
