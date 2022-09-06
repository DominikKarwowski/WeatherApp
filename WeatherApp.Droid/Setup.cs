using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Android.Core;
using Serilog;
using Serilog.Extensions.Logging;

namespace DjK.WeatherApp.Droid
{
    public class Setup : MvxAndroidSetup<Core.App>
    {
        protected override ILoggerFactory CreateLogFactory()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .CreateLogger();

            return new SerilogLoggerFactory();
        }

        protected override ILoggerProvider CreateLogProvider() => new SerilogLoggerProvider();
    }

}