using DjK.WeatherApp.Core.Helpers;
using DjK.WeatherApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.ViewModels
{
    /// <summary>
    /// ViewModel for WeatherDetails.
    /// </summary>
    public class WeatherDetailsViewModel : MvxViewModel<WeatherDetails>
    {
        private readonly IMvxNavigationService _navigationService;
        private WeatherDetails _weatherDetails;

        /// <summary>
        /// Name of the city.
        /// </summary>
        public string CityName
        {
            get { return _weatherDetails.CityName; }
            set
            {
                if (_weatherDetails.CityName == value) return;
                _weatherDetails.CityName = value;
                RaisePropertyChanged(nameof(CityName));
            }
        }

        /// <summary>
        /// Weather condition description.
        /// </summary>
        public string Description
        {
            get { return _weatherDetails.Description; }
            set
            {
                if (_weatherDetails.Description == value) return;
                _weatherDetails.Description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        /// <summary>
        /// Temperature.
        /// </summary>
        public double Temperature
        {
            get { return _weatherDetails.Temperature.RoundToOneDecimalPlace(); }
            set
            {
                if (_weatherDetails.Temperature == value) return;
                _weatherDetails.Temperature = value;
                RaisePropertyChanged(nameof(Temperature));
            }
        }

        /// <summary>
        /// Temperature according to the human perception of weather.
        /// </summary>
        public double TemperatureFeelsLike
        {
            get { return _weatherDetails.TemperatureFeelsLike.RoundToOneDecimalPlace(); }
            set
            {
                if (_weatherDetails.TemperatureFeelsLike == value) return;
                _weatherDetails.TemperatureFeelsLike = value;
                RaisePropertyChanged(nameof(TemperatureFeelsLike));
            }
        }

        /// <summary>
        /// Minimum temperature at the moment.
        /// </summary>
        public double TemperatureMin
        {
            get { return _weatherDetails.TemperatureMin.RoundToOneDecimalPlace(); }
            set
            {
                if (_weatherDetails.TemperatureMin == value) return;
                _weatherDetails.TemperatureMin = value;
                RaisePropertyChanged(nameof(TemperatureMin));
            }
        }

        /// <summary>
        /// Maximum temperature at the moment.
        /// </summary>
        public double TemperatureMax
        {
            get { return _weatherDetails.TemperatureMax.RoundToOneDecimalPlace(); }
            set
            {
                if (_weatherDetails.TemperatureMax == value) return;
                _weatherDetails.TemperatureMax = value;
                RaisePropertyChanged(nameof(TemperatureMax));
            }
        }

        /// <summary>
        /// Maximum temperature at the moment.
        /// </summary>
        public string TemperatureUnit
        {
            get { return _weatherDetails.TemperatureUnit; }
            set
            {
                if (_weatherDetails.TemperatureUnit == value) return;
                _weatherDetails.TemperatureUnit = value;
                RaisePropertyChanged(nameof(TemperatureUnit));
            }
        }

        /// <summary>
        /// Command to close the viewModel.
        /// </summary>
        public IMvxAsyncCommand CloseCommand => new MvxAsyncCommand(Close);

        /// <summary>
        /// Creates ViewModel instance.
        /// </summary>
        /// <param name="navigationService">Navigation service.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public WeatherDetailsViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        /// <summary>
        /// Required to set underlying model for the viewModel.
        /// </summary>
        /// <param name="weatherDetails"></param>
        public override void Prepare(WeatherDetails weatherDetails)
        {
            _weatherDetails = weatherDetails;
        }

        private async Task Close()
        {
            await _navigationService.Close(this);
        }
    }
}
