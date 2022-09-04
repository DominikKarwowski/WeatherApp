namespace DjK.WeatherApp.Core.Models
{
    /// <summary>
    /// Contains weather data.
    /// </summary>
    public class WeatherDetails
    {
        /// <summary>
        /// Weather condition description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Temperature.
        /// </summary>
        public float Temperature { get; set; }

        /// <summary>
        /// Temperature according to the human perception of weather.
        /// </summary>
        public float TemperatureFeelsLike { get; set; }

        /// <summary>
        /// Minimum temperature at the moment.
        /// </summary>
        public float TemperatureMin { get; set; }

        /// <summary>
        /// Maximum temperature at the moment.
        /// </summary>
        public float TemperatureMax { get; set; }

    }
}
