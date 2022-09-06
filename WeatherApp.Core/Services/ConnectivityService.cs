using DjK.WeatherApp.Core.Services.Abstractions;
using Xamarin.Essentials;

namespace DjK.WeatherApp.Core.Services
{
    /// <summary>
    /// Connectivity service. Wrapper for Xamarin.Essentials.Connectivity.
    /// </summary>
    public class ConnectivityService : IConnectivityService
    {
        /// <summary>
        /// Checks if active internet connection is available.
        /// </summary>
        /// <returns>True if active internet connection is available, false otherwise.</returns>
        public bool IsActiveInternetConnection() =>
            Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
