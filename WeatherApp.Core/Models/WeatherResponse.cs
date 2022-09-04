using System;
using System.Collections.Generic;
using System.Text;

namespace DjK.WeatherApp.Core.Models
{
    public class WeatherResponse
    {
        /// <summary>
        /// Weather details.
        /// </summary>
        public WeatherDetails WeatherDetails { get; }
        
        /// <summary>
        /// Response from a service retrieving weather data. Can contain error details in case the data could not be obtained.
        /// </summary>
        public string ResponseMessage { get; }
        
        /// <summary>
        /// Describes if data were correctly retrieved.
        /// </summary>
        public bool IsSuccessful { get; }

        /// <summary>
        /// Response received from an IWeatherService.
        /// </summary>
        /// <param name="weatherDetails">Obtained weather details.</param>
        /// <param name="responseMessage">Response that accompanies the weather details.</param>
        /// <param name="isSuccessful">Response status.</param>
        public WeatherResponse(WeatherDetails weatherDetails, string responseMessage, bool isSuccessful)
        {
            WeatherDetails = weatherDetails;
            ResponseMessage = responseMessage;
            IsSuccessful = isSuccessful;
        }
    }
}
