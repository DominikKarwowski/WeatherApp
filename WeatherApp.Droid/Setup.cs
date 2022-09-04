using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Android.Core;

namespace DjK.WeatherApp.Droid
{
    public class Setup : MvxAndroidSetup<Core.App>
    {
        protected override ILoggerFactory CreateLogFactory()
        {
            // TODO: implement actual log factory
            return null;
        }

        protected override ILoggerProvider CreateLogProvider()
        {
            // TODO: implement actual log provider
            return null;
        }
    }
}