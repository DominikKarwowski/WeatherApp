namespace DjK.WeatherApp.Core.Models
{
    /// <summary>
    /// Contains set of parameters needed to put a request for weather data.
    /// </summary>
    public class WeatherRequestParameters
    {
        /// <summary>
        /// Name of the city for which the weather data are requested.
        /// </summary>
        public string CityName { get; }

        /// <summary>
        /// Language in which the weather data are requested.
        /// </summary>
        public string Language { get; }

        /// <summary>
        /// True to request data in metric units. False to request data in imperial units.
        /// </summary>
        public bool IsMetric { get; }

        /// <summary>
        /// Creates a WeatherRequest instance.
        /// </summary>
        /// <param name="cityName">Name of the city for which the weather data are requested.</param>
        /// <param name="language">Language in which the weather data are requested.</param>
        /// <param name="isMetric">True to request data in metric units. False to request data in imperial units.</param>
        public WeatherRequestParameters(string cityName, string language, bool isMetric)
        {
            CityName = cityName;
            Language = language;
            IsMetric = isMetric;
        }
    }
}
