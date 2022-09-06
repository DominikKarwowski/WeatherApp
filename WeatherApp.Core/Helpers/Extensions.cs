using System;

namespace DjK.WeatherApp.Core.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Rounds a number to one decimal place.
        /// </summary>
        /// <param name="number">Number to be rounded.</param>
        /// <returns>Number rounded to one decimal place.</returns>
        public static double RoundToOneDecimalPlace(this double number) => Math.Round(number, 1);
    }
}
