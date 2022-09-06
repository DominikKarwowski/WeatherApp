using DjK.WeatherApp.Core.Services;
using DjK.WeatherApp.Core.Services.Abstractions;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DjK.WeatherApp.Core.ViewModels
{
    /// <summary>
    /// ViewModel for a HomeView.
    /// </summary>
    public class HomeViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IWeatherService _weatherService;
        private readonly IFavouritiesService _favouritiesService;
        private readonly MvxInteraction<string> _interactionForCitySaved;

        private bool _showProgress;
        private string _cityName;
        private string _errorMessage;

        /// <summary>
        /// Indicates visibility for a Progress Bar in a related view.
        /// </summary>
        public bool ShowProgress
        {
            get { return _showProgress; }
            set { SetProperty(ref _showProgress, value); }
        }

        /// <summary>
        /// Name of the city for which weather data will be shown.
        /// </summary>
        public string CityName
        {
            get { return _cityName; }
            set { SetProperty(ref _cityName, value); }
        }

        /// <summary>
        /// Error message to be displayed in case of unsuccessful request.
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        /// <summary>
        /// Language in which weather data will be provided. Inherited from the system settings.
        /// </summary>
        public string Language { get; private set; }

        /// <summary>
        /// Units in which weather data will be provided. Inherited from a system culture settings.
        /// </summary>
        public bool IsMetric { get; private set; }

        /// <summary>
        /// Command to retrieve and show weather data.
        /// </summary>
        public IMvxAsyncCommand ShowWeatherDetailsCommand => new MvxAsyncCommand(ShowWeatherDetails);

        /// <summary>
        /// Command to save provided city name as a favourite.
        /// </summary>
        public IMvxAsyncCommand SaveFavouriteCityCommand => new MvxAsyncCommand(SaveFavouriteCity);

        /// <summary>
        /// Command to set current culture.
        /// </summary>
        public IMvxCommand<CultureInfo> SetCurrentCultureCommand => new MvxCommand<CultureInfo>(SetCurrentCulture);

        /// <summary>
        /// Interaction with related view to raise info to the user about favourite city saving.
        /// </summary>
        public IMvxInteraction<string> InteractionForCitySaved => _interactionForCitySaved;

        /// <summary>
        /// Creates HomeViewModel instance.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        /// <param name="weatherService">Weather service.</param>
        /// <param name="favouritiesService">Favourities service.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public HomeViewModel(IMvxNavigationService navigationService, IWeatherService weatherService,
            IFavouritiesService favouritiesService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
            _favouritiesService = favouritiesService ?? throw new ArgumentNullException(nameof(favouritiesService));
            _interactionForCitySaved = new MvxInteraction<string>();
        }

        /// <summary>
        /// Performs viewModel initialization.
        /// </summary>
        /// <returns></returns>
        public override async Task Initialize()
        {
            await base.Initialize();
            if (string.IsNullOrWhiteSpace(CityName))
                await LoadFavouriteCity();
        }

        protected override void SaveStateToBundle(IMvxBundle bundle)
        {
            base.SaveStateToBundle(bundle);
            bundle.Data["CityName"] = CityName;
        }

        protected override void ReloadFromBundle(IMvxBundle state)
        {
            base.ReloadFromBundle(state);
            CityName = state.Data["CityName"];
        }


        private async Task SaveFavouriteCity()
        {
            await _favouritiesService.SaveFavouriteCity(CityName);
            _interactionForCitySaved.Raise(CityName);
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
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    ErrorMessage = "No internet connection";
                    return;
                }

                ShowProgress = true;
                var weatherResponse =
                    await _weatherService.GetWeatherResponseForLocation(CityName, Language, IsMetric);
                ShowProgress = false;

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
