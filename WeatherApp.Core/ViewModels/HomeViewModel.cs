﻿using DjK.WeatherApp.Core.Models;
using DjK.WeatherApp.Core.Services;
using DjK.WeatherApp.Core.Services.Abstractions;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IWeatherService _weatherService;
        private readonly IFavouritiesService _favouritiesService;

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

        public string Language { get; private set; }
        public bool IsMetric { get; private set; }

        public IMvxAsyncCommand ShowWeatherDetailsCommand => new MvxAsyncCommand(ShowWeatherDetails);
        public IMvxAsyncCommand SaveFavouriteCityCommand => new MvxAsyncCommand(SaveFavouriteCity);

        public IMvxCommand<CultureInfo> SetCurrentCultureCommand => new MvxCommand<CultureInfo>(SetCurrentCulture);

        public HomeViewModel(IMvxNavigationService navigationService, IWeatherService weatherService,
            IFavouritiesService favouritiesService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
            _favouritiesService = favouritiesService ?? throw new ArgumentNullException(nameof(favouritiesService));
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await LoadFavouriteCity();
        }

        private async Task SaveFavouriteCity()
        {
            await _favouritiesService.SaveFavouriteCity(CityName);
        }

        private async Task LoadFavouriteCity()
        {
            CityName = await _favouritiesService.LoadFavouriteCity();
        }

        private void SetCurrentCulture(CultureInfo currentCulture)
        {
            try
            {
                Language = currentCulture.TwoLetterISOLanguageName;
                var regionInfo = new RegionInfo(currentCulture.LCID);
                IsMetric = regionInfo.IsMetric;
            }
            catch (Exception)
            {
                // TODO: add logging
                Language = "en";
                IsMetric = true;
            }
        }

        private async Task ShowWeatherDetails()
        {
            try
            {
                var weatherResponse =
                    await _weatherService.GetWeatherResponseForLocation(CityName, Language, IsMetric);
                if (weatherResponse.IsSuccessful)
                {
                    ErrorMessage = string.Empty;
                    weatherResponse.WeatherDetails.TemperatureUnit = IsMetric ? "°C" : "°F";
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
