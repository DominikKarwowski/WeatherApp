using DjK.WeatherApp.Core.Models;
using DjK.WeatherApp.Core.Services;
using DjK.WeatherApp.Core.Services.Abstractions;
using DjK.WeatherApp.Core.ViewModels;
using MvvmCross.Navigation;
using NUnit.Framework;
using System.Globalization;

namespace WeatherApp.Core.Tests.ViewModels
{
    [TestFixture]
    public class HomeViewModelTests
    {

        Mock<IMvxNavigationService> navigationServiceFake;
        Mock<IWeatherService> weatherServiceFake;
        Mock<IFavouritiesService> favouritiesServiceFake;

        [SetUp]
        public void SetUp()
        {
            navigationServiceFake = new Mock<IMvxNavigationService>();
            weatherServiceFake = new Mock<IWeatherService>();
            favouritiesServiceFake = new Mock<IFavouritiesService>();
        }

        [Test]
        public void Creating_HomeViewModel_loads_favourite_city()
        {
            // Arrange
            const string testCityName = "Test CityName";
            favouritiesServiceFake.Setup(s => s.LoadFavouriteCity()).ReturnsAsync(testCityName);
            
            // Act
            var sut = CreateHomeViewModel();

            // Assert
            Assert.That(sut.CityName, Is.EqualTo(testCityName));
        }

        [Test]
        public void Setting_CityName_RaisesPropertyChanged()
        {
            // Arrange
            var propertyChangedRaised = false;
            var sut = CreateHomeViewModel();
            sut.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
            sut.PropertyChanged += (s, e) => propertyChangedRaised = (e.PropertyName == nameof(HomeViewModel.CityName));

            // Act
            sut.CityName = "New CityName";

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }

        [Test]
        public void Setting_ErrorMessage_RaisesPropertyChanged()
        {
            // Arrange
            var propertyChangedRaised = false;
            var sut = CreateHomeViewModel();
            sut.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
            sut.PropertyChanged += (s, e) => propertyChangedRaised = (e.PropertyName == nameof(HomeViewModel.ErrorMessage));

            // Act
            sut.ErrorMessage = "New error message";

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }


        [Test]
        public void Saving_favourite_city_stores_CityName()
        {
            // Arrange
            const string testCityName = "Test CityName";
            favouritiesServiceFake.Setup(s => s.LoadFavouriteCity()).ReturnsAsync(testCityName);
            var sut = CreateHomeViewModel();

            // Act
            sut.SaveFavouriteCityCommand.Execute();

            // Assert
            favouritiesServiceFake
                .Verify(s => s.SaveFavouriteCity(
                    It.Is<string>(c => c == testCityName)),
                Times.Once);
        }

        [Test]
        public void Set_current_culture_command_sets_language_and_unit_system_info()
        {
            // Arrange
            const string testCityName = "Test CityName";
            const string testLanguage = "en";
            const bool testIsMetric = true;
            const string testReasonPhrase = "Test reason phrase";
            const bool testIsSuccessful = true;

            var weatherDetails = new WeatherDetails();
            var weatherResponse = new WeatherResponse(weatherDetails, testReasonPhrase, testIsSuccessful);

            favouritiesServiceFake.Setup(s => s.LoadFavouriteCity()).ReturnsAsync(testCityName);
            weatherServiceFake.Setup(s => s.GetWeatherResponseForLocation(testCityName, testLanguage, testIsMetric)).ReturnsAsync(weatherResponse);
            var sut = CreateHomeViewModel();

            // Act
            sut.SetCurrentCultureCommand.Execute(new CultureInfo("en-GB"));

            // Assert
            Assert.That(sut.Language, Is.EqualTo("en"));
            Assert.That(sut.IsMetric, Is.EqualTo(true));
        }

        [Test]
        public void Show_weather_details_command_retrieves_weather_details_and_passes_them_to_WeatherDetailsViewModel()
        {
            // Arrange
            const string testCityName = "Test CityName";
            const string testLanguage = "en";
            const bool testIsMetric = true;
            const string testDescription = "Some description";
            const double testTemperature = 100;
            const double testTemperatureFeelsLike = 103;
            const double testTemperatureMin = 95;
            const double testTemperatureMax = 105;
            const string testTemperatureUnit = "U";
            const string testReasonPhrase = "Test reason phrase";
            const bool testIsSuccessful = true;

            var weatherDetails = new WeatherDetails()
            {
                CityName = testCityName,
                Description = testDescription,
                Temperature = testTemperature,
                TemperatureFeelsLike = testTemperatureFeelsLike,
                TemperatureMin = testTemperatureMin,
                TemperatureMax = testTemperatureMax,
                TemperatureUnit = testTemperatureUnit,
            };

            var weatherResponse = new WeatherResponse(weatherDetails, testReasonPhrase, testIsSuccessful);

            favouritiesServiceFake.Setup(s => s.LoadFavouriteCity()).ReturnsAsync(testCityName);
            weatherServiceFake.Setup(s => s.GetWeatherResponseForLocation(testCityName, testLanguage, testIsMetric)).ReturnsAsync(weatherResponse);
            var sut = CreateHomeViewModel();
            sut.SetCurrentCultureCommand.Execute(new CultureInfo("en-GB"));

            // Act
            sut.ShowWeatherDetailsCommand.Execute();

            // Assert
            navigationServiceFake
                .Verify(n => n.Navigate(
                    It.Is<Type>(t => t == typeof(WeatherDetailsViewModel)),
                    It.Is<WeatherDetails>(w =>
                        w.CityName == weatherDetails.CityName
                        && w.Description == weatherDetails.Description
                        && w.Temperature == weatherDetails.Temperature
                        && w.TemperatureFeelsLike == weatherDetails.TemperatureFeelsLike
                        && w.TemperatureMin == weatherDetails.TemperatureMin
                        && w.TemperatureMax == weatherDetails.TemperatureMax
                        && w.TemperatureUnit == weatherDetails.TemperatureUnit),
                    default,
                    default),
                Times.Once);
        }

        [Test]
        public void Successful_response_from_weather_service_empties_error_message()
        {
            // Arrange
            const string testCityName = "Test CityName";
            const string testLanguage = "en";
            const bool testIsMetric = true;
            const string testReasonPhrase = "Test reason phrase";
            const bool testIsSuccessful = true;

            var weatherDetails = new WeatherDetails();
            var weatherResponse = new WeatherResponse(weatherDetails, testReasonPhrase, testIsSuccessful);

            favouritiesServiceFake.Setup(s => s.LoadFavouriteCity()).ReturnsAsync(testCityName);
            weatherServiceFake.Setup(s => s.GetWeatherResponseForLocation(testCityName, testLanguage, testIsMetric)).ReturnsAsync(weatherResponse);
            var sut = CreateHomeViewModel();
            sut.SetCurrentCultureCommand.Execute(new CultureInfo("en-GB"));
            sut.ErrorMessage = "Test Error Message";

            // Act
            sut.ShowWeatherDetailsCommand.Execute();

            // Assert
            Assert.That(sut.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Show_weather_details_command_on_unsuccessful_response_does_not_navigate_to_WeatherDetailsViewModel()
        {
            // Arrange
            const string testCityName = "Test CityName";
            const string testLanguage = "en";
            const bool testIsMetric = true;
            const string testReasonPhrase = "Test reason phrase";
            const bool testIsSuccessful = false;

            var weatherDetails = new WeatherDetails();
            var weatherResponse = new WeatherResponse(weatherDetails, testReasonPhrase, testIsSuccessful);

            favouritiesServiceFake.Setup(s => s.LoadFavouriteCity()).ReturnsAsync(testCityName);
            weatherServiceFake.Setup(s => s.GetWeatherResponseForLocation(testCityName, testLanguage, testIsMetric)).ReturnsAsync(weatherResponse);
            var sut = CreateHomeViewModel();
            sut.SetCurrentCultureCommand.Execute(new CultureInfo("en-GB"));

            // Act
            sut.ShowWeatherDetailsCommand.Execute();

            // Assert
            navigationServiceFake
                .Verify(n => n.Navigate(
                    typeof(WeatherDetailsViewModel),
                    It.IsAny<WeatherDetails>(),
                    default,
                    default),
                Times.Never);
        }

        [Test]
        public void Show_weather_details_command_on_unsuccessful_response_sets_ErrorMessage()
        {
            // Arrange
            const string testCityName = "Test CityName";
            const string testLanguage = "en";
            const bool testIsMetric = true;
            const string testReasonPhrase = "Test reason phrase";
            const bool testIsSuccessful = false;

            var weatherDetails = new WeatherDetails();
            var weatherResponse = new WeatherResponse(weatherDetails, testReasonPhrase, testIsSuccessful);

            favouritiesServiceFake.Setup(s => s.LoadFavouriteCity()).ReturnsAsync(testCityName);
            weatherServiceFake.Setup(s => s.GetWeatherResponseForLocation(testCityName, testLanguage, testIsMetric)).ReturnsAsync(weatherResponse);
            var sut = CreateHomeViewModel();
            sut.SetCurrentCultureCommand.Execute(new CultureInfo("en-GB"));

            // Act
            sut.ShowWeatherDetailsCommand.Execute();

            // Assert
            Assert.That(sut.ErrorMessage, Is.EqualTo(testReasonPhrase));
        }

        [Test]
        public void Show_weather_details_command_on_exception_sets_generic_ErrorMessage()
        {
            // Arrange
            const string testCityName = "Test CityName";
            const string testLanguage = "en";
            const bool testIsMetric = true;

            var weatherDetails = new WeatherDetails();

            favouritiesServiceFake.Setup(s => s.LoadFavouriteCity()).ReturnsAsync(testCityName);
            weatherServiceFake.Setup(s => s.GetWeatherResponseForLocation(testCityName, testLanguage, testIsMetric)).Throws<Exception>();
            var sut = CreateHomeViewModel();
            sut.SetCurrentCultureCommand.Execute(new CultureInfo("en-GB"));

            // Act
            sut.ShowWeatherDetailsCommand.Execute();

            // Assert
            Assert.That(sut.ErrorMessage, Is.EqualTo("Unexpected exception"));
        }



        private HomeViewModel CreateHomeViewModel()
        {
            var homeViewModel = new HomeViewModel(navigationServiceFake.Object, weatherServiceFake.Object, favouritiesServiceFake.Object);
            homeViewModel.Initialize().Wait();
            return homeViewModel;
        }

    }
}
