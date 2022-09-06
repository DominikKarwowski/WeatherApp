namespace DjK.WeatherApp.Core.Models
{
    /// <summary>
    /// Wrapper class for WeatherDetails and additional information about a request from a weather data provider.
    /// </summary>
    public class WeatherResponse
    {
        /// <summary>
        /// Weather details.
        /// </summary>
        public WeatherDetails WeatherDetails { get; }
        
        /// <summary>
        /// Reason phrase from a service retrieving weather data. Can contain error details in case the data could not be obtained.
        /// </summary>
        public string ReasonPhrase { get; }
        
        /// <summary>
        /// Describes if data were correctly retrieved.
        /// </summary>
        public bool IsSuccessful { get; }

        /// <summary>
        /// Response received from an IWeatherService.
        /// </summary>
        /// <param name="weatherDetails">Obtained weather details.</param>
        /// <param name="reasonPhrase">Reason phrase that accompanies the weather details response.</param>
        /// <param name="isSuccessful">Response status.</param>
        public WeatherResponse(WeatherDetails weatherDetails, string reasonPhrase, bool isSuccessful)
        {
            WeatherDetails = weatherDetails;
            ReasonPhrase = reasonPhrase;
            IsSuccessful = isSuccessful;
        }
    }
}
