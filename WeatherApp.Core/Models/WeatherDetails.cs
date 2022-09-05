namespace DjK.WeatherApp.Core.Models
{
    /// <summary>
    /// Contains weather data.
    /// </summary>
    public class WeatherDetails
    {
        /// <summary>
        /// Name of the city.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Weather condition description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Temperature.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Temperature according to the human perception of weather.
        /// </summary>
        public double TemperatureFeelsLike { get; set; }

        /// <summary>
        /// Minimum temperature at the moment.
        /// </summary>
        public double TemperatureMin { get; set; }

        /// <summary>
        /// Maximum temperature at the moment.
        /// </summary>
        public double TemperatureMax { get; set; }

        /// <summary>
        /// Temperature unit.
        /// </summary>
        public string TemperatureUnit { get; set; }
    }
}
