using System;
using Xamarin.UITest;

namespace WeatherApp.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .EnableLocalScreenshots()
                    .ApkFile("../WeatherApp.Droid/bin/Release/com.djk.weatherapp.droid-Signed.apk")
                    .StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}