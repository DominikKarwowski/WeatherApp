using System;
using System.Collections.Generic;
using System.Text;

namespace DjK.WeatherApp.Core.Helpers
{
    public static class Extensions
    {
        public static double RoundToOneDecimalPlace(this double number) => Math.Round(number, 1);
    }
}
