using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DjK.WeatherApp.Droid.Logging
{
    public class DroidLoggerFactory : ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider)
        {
            // TODO: implement actual logger provider
        }

        public ILogger CreateLogger(string categoryName) => new DroidLogger();

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }

}