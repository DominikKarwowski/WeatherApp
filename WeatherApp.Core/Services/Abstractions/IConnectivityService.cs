namespace DjK.WeatherApp.Core.Services.Abstractions
{
    /// <summary>
    /// Represents a connectivity service.
    /// </summary>
    public interface IConnectivityService
    {
        /// <summary>
        /// Represents a method to check if active internet connection is available.
        /// </summary>
        /// <returns>True if active internet connection is available, false otherwise.</returns>
        bool IsActiveInternetConnection();
    }
}
