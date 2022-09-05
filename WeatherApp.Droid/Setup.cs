using DjK.WeatherApp.Droid.Logging;
using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Android.Core;
using System;

namespace DjK.WeatherApp.Droid
{
    public class Setup : MvxAndroidSetup<Core.App>
    {
        protected override ILoggerFactory CreateLogFactory() => new DroidLoggerFactory();

        protected override ILoggerProvider CreateLogProvider() => new DroidLoggerProvider();
    }



}