using DjK.WeatherApp.Core.Models;
using DjK.WeatherApp.Core.ViewModels;
using MvvmCross.Navigation;

namespace WeatherApp.Core.Tests.ViewModels
{
    [TestFixture]
    public class WeatherDetailsViewModelTests
    {
        Mock<IMvxNavigationService> navigationServiceFake;

        [SetUp]
        public void SetUp()
        {
            navigationServiceFake = new Mock<IMvxNavigationService>();
        }


        [Test]
        public void Values_of_properties_containing_weather_data_come_from_WeatherDetails()
        {
            // Arrange
            const string testCityName = "Some City";
            const string testDescription = "Some description";
            const double testTemperature = 100;
            const double testTemperatureFeelsLike = 103;
            const double testTemperatureMin = 95;
            const double testTemperatureMax = 105;
            const string testTemperatureUnit = "U";

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

            var sut = CreateWeatherDetailsViewModel();

            // Act
            sut.Prepare(weatherDetails);

            Assert.Multiple(() =>
            {
                Assert.That(sut.CityName, Is.EqualTo(testCityName));
                Assert.That(sut.Description, Is.EqualTo(testDescription));
                Assert.That(sut.Temperature, Is.EqualTo(testTemperature));
                Assert.That(sut.TemperatureFeelsLike, Is.EqualTo(testTemperatureFeelsLike));
                Assert.That(sut.TemperatureMin, Is.EqualTo(testTemperatureMin));
                Assert.That(sut.TemperatureMax, Is.EqualTo(testTemperatureMax));
                Assert.That(sut.TemperatureUnit, Is.EqualTo(testTemperatureUnit));
            });
        }

        [Test]
        public void Setting_CityName_RaisesPropertyChanged()
        {
            // Arrange
            var weatherDetails = new WeatherDetails { CityName = "Test CityName" };
            var propertyChangedRaised = false;
            var sut = CreateWeatherDetailsViewModel();
            sut.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
            sut.PropertyChanged += (s, e) => propertyChangedRaised = (e.PropertyName == nameof(WeatherDetailsViewModel.CityName));
            sut.Prepare(weatherDetails);

            // Act
            sut.CityName = "New CityName";

            Assert.That(propertyChangedRaised, Is.True);
        }

        [Test]
        public void Setting_Description_RaisesPropertyChanged()
        {
            // Arrange
            var weatherDetails = new WeatherDetails { Description = "Test description" };
            var propertyChangedRaised = false;
            var sut = CreateWeatherDetailsViewModel();
            sut.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
            sut.PropertyChanged += (s, e) => propertyChangedRaised = (e.PropertyName == nameof(WeatherDetailsViewModel.Description));
            sut.Prepare(weatherDetails);

            // Act
            sut.Description = "New description";

            Assert.That(propertyChangedRaised, Is.True);
        }

        [Test]
        public void Setting_Temperature_RaisesPropertyChanged()
        {
            // Arrange
            var weatherDetails = new WeatherDetails { Temperature = 100 };
            var propertyChangedRaised = false;
            var sut = CreateWeatherDetailsViewModel();
            sut.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
            sut.PropertyChanged += (s, e) => propertyChangedRaised = (e.PropertyName == nameof(WeatherDetailsViewModel.Temperature));
            sut.Prepare(weatherDetails);

            // Act
            sut.Temperature = 102;

            Assert.That(propertyChangedRaised, Is.True);
        }

        [Test]
        public void Setting_TemperatureFeelsLike_RaisesPropertyChanged()
        {
            // Arrange
            var weatherDetails = new WeatherDetails { TemperatureFeelsLike = 100 };
            var propertyChangedRaised = false;
            var sut = CreateWeatherDetailsViewModel();
            sut.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
            sut.PropertyChanged += (s, e) => propertyChangedRaised = (e.PropertyName == nameof(WeatherDetailsViewModel.TemperatureFeelsLike));
            sut.Prepare(weatherDetails);

            // Act
            sut.TemperatureFeelsLike = 102;

            Assert.That(propertyChangedRaised, Is.True);
        }

        [Test]
        public void Setting_TemperatureMin_RaisesPropertyChanged()
        {
            // Arrange
            var weatherDetails = new WeatherDetails { TemperatureMin = 100 };
            var propertyChangedRaised = false;
            var sut = CreateWeatherDetailsViewModel();
            sut.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
            sut.PropertyChanged += (s, e) => propertyChangedRaised = (e.PropertyName == nameof(WeatherDetailsViewModel.TemperatureMin));
            sut.Prepare(weatherDetails);

            // Act
            sut.TemperatureMin = 102;

            Assert.That(propertyChangedRaised, Is.True);
        }

        [Test]
        public void Setting_TemperatureMax_RaisesPropertyChanged()
        {
            // Arrange
            var weatherDetails = new WeatherDetails { TemperatureMax = 100 };
            var propertyChangedRaised = false;
            var sut = CreateWeatherDetailsViewModel();
            sut.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
            sut.PropertyChanged += (s, e) => propertyChangedRaised = (e.PropertyName == nameof(WeatherDetailsViewModel.TemperatureMax));
            sut.Prepare(weatherDetails);

            // Act
            sut.TemperatureMax = 102;

            Assert.That(propertyChangedRaised, Is.True);
        }

        [Test]
        public void Setting_TemperatureUnit_RaisesPropertyChanged()
        {
            // Arrange
            var weatherDetails = new WeatherDetails { TemperatureUnit = "F" };
            var propertyChangedRaised = false;
            var sut = CreateWeatherDetailsViewModel();
            sut.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false);
            sut.PropertyChanged += (s, e) => propertyChangedRaised = (e.PropertyName == nameof(WeatherDetailsViewModel.TemperatureUnit));
            sut.Prepare(weatherDetails);

            // Act
            sut.TemperatureUnit = "C";

            Assert.That(propertyChangedRaised, Is.True);
        }

        [Test]
        public void ViewModel_closes_on_command()
        {
            // Arrange
            var sut = CreateWeatherDetailsViewModel();
            sut.Prepare(new WeatherDetails());

            // Act
            sut.CloseCommand.Execute();

            // Assert
            navigationServiceFake.Verify(s => s.Close(sut, default), Times.Once);
        }

        private WeatherDetailsViewModel CreateWeatherDetailsViewModel() =>
            new(navigationServiceFake.Object);
    }
}
