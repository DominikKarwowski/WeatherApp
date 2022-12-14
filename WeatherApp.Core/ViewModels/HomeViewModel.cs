using DjK.WeatherApp.Core.Models;
using DjK.WeatherApp.Core.Services;
using DjK.WeatherApp.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.ViewModels
{
    /// <summary>
    /// ViewModel for a HomeView.
    /// </summary>
    public class HomeViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IWeatherServiceWeb _weatherService;
        private readonly IFavouritiesService _favouritiesService;
        private readonly IConnectivityService _connectivityService;
        private readonly ILogger<HomeViewModel> _logger;
        private readonly MvxInteraction<string> _interactionForCitySaved;

        private CancellationTokenSource cancellationTokenSource;
        private bool _showProgress;
        private string _cityName;
        private string _errorMessage;

        /// <summary>
        /// Indicates visibility for a Progress Bar in a related view.
        /// </summary>
        public bool RequestInProgress
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

        public IMvxCommand CancelRequestCommand => new MvxCommand(CancelRequest);

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
        /// <param name="connectivityService">Favourities service.</param>
        /// <param name="logger">Logger implementation.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public HomeViewModel(IMvxNavigationService navigationService, IWeatherServiceWeb weatherService,
            IFavouritiesService favouritiesService, IConnectivityService connectivityService, ILogger<HomeViewModel> logger)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
            _favouritiesService = favouritiesService ?? throw new ArgumentNullException(nameof(favouritiesService));
            _connectivityService = connectivityService ?? throw new ArgumentNullException(nameof(connectivityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Language = "en";
                IsMetric = true;
            }
        }

        private async Task ShowWeatherDetails()
        {
            try
            {
                if (!_connectivityService.IsActiveInternetConnection())
                {
                    ErrorMessage = "No Internet connection";
                    return;
                }

                await TryRetrieveAndShowWeatherData();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ErrorMessage = "Oops, something went wrong...";
            }
        }

        private async Task TryRetrieveAndShowWeatherData()
        {
            try
            {
                cancellationTokenSource = new CancellationTokenSource();
                RequestInProgress = true;
                var weatherResponse = await _weatherService.GetWeatherResponse(
                        new WeatherRequestParameters(CityName, Language, IsMetric),
                        cancellationTokenSource.Token);

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
            catch (OperationCanceledException) when (cancellationTokenSource.IsCancellationRequested)
            {
                ErrorMessage = "Request has been cancelled";
            }
            catch (OperationCanceledException)
            {
                ErrorMessage = "Request timeout";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cancellationTokenSource.Dispose();
                RequestInProgress = false;
            }
        }

        private void CancelRequest()
        {
            try
            {
                cancellationTokenSource?.Cancel();
            }
            catch (ObjectDisposedException)
            { }
        }



    }
}
