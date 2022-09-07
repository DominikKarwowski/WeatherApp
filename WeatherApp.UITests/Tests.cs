using NUnit.Framework;
using System;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace WeatherApp.UITests
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }


        [Test]
        public void HomeView_is_displayed()
        {
            // Act
            AppResult[] results = app.WaitForElement(q => q.Marked("city_input"));
            app.Screenshot("HomeView");

            Assert.That(results.Any(), Is.True);
        }


        [Test]
        public void Insert_empty_city_name_shows_Bad_Request_error()
        {
            // Arrange
            app.EnterText(q => q.Marked("city_input_text_field"), string.Empty);
            app.Tap(q => q.Marked("get_weather"));

            // Act
            AppResult[] results = app.WaitForElement(q =>
                q.Marked("city_input").Property("Error").StartsWith("Bad Request")
                    );
            app.Screenshot("BadRequest");

            Assert.That(results.Any(), Is.True);
        }

        [Test]
        public void Insert_correct_city_name_opens_weather_details()
        {
            // Arrange
            const string cityName = "warsaw";
            app.EnterText(q => q.Marked("city_input_text_field"), cityName);

            // Act
            app.Tap(q => q.Marked("get_weather"));

            AppResult[] results = app.WaitForElement(q => q.Marked("location"));
            app.Screenshot("WeatherDetailsView");

            Assert.That(results.Any(), Is.True);
        }

        [Test]
        public void Change_in_screen_orientation_preserves_inserted_city_name()
        {
            // Arrange
            const string cityName = "warsaw";
            app.SetOrientationPortrait();
            app.ClearText(q => q.Marked("city_input_text_field"));
            app.EnterText(q => q.Marked("city_input_text_field"), cityName);

            // Act
            app.SetOrientationLandscape();

            AppResult[] results = app.WaitForElement(
                q => q.Marked("city_input_text_field").Text(cityName));
            app.Screenshot("CityNamePreserved");

            Assert.That(results.Any(), Is.True);
        }

        [Test]
        public void Change_in_screen_orientation_in_weather_details_preserves_inserted_city_name()
        {
            // Arrange
            const string cityName = "warsaw";
            app.SetOrientationPortrait();
            app.ClearText(q => q.Marked("city_input_text_field"));
            app.EnterText(q => q.Marked("city_input_text_field"), cityName);
            app.Tap(q => q.Marked("get_weather"));
            app.WaitForElement(q => q.Marked("location"));

            // Act
            app.SetOrientationLandscape();
            app.Back();

            AppResult[] results = app.WaitForElement(
                q => q.Marked("city_input_text_field").Text(cityName));
            app.Screenshot("CityNamePreserved");

            Assert.That(results.Any(), Is.True);
        }

    }
}
