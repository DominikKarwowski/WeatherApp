using DjK.WeatherApp.Core.Models;
using DjK.WeatherApp.Core.Services;
using DjK.WeatherApp.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WeatherApp.Core.Tests.Services
{
    [TestFixture]
    public class OpenWeatherServiceTests
    {

        Mock<IRestServiceWeb> restServiceFake;
        Mock<ILogger<OpenWeatherServiceWeb>> loggerFake;

        [SetUp]
        public void Setup()
        {
            restServiceFake = new Mock<IRestServiceWeb>();
            loggerFake = new Mock<ILogger<OpenWeatherServiceWeb>>();
        }

        [Test]
        public async Task WeatherService_gets_response_and_weather_details_for_specified_location_on_successful_request()
        {
            // Arrange
            const string jsonResponse = "{\"coord\":{\"lon\":10.99,\"lat\":44.34},\"weather\":[{\"id\":803,\"main\":\"Clouds\",\"description\":\"broken clouds\",\"icon\":\"04d\"}],\"base\":\"stations\",\"main\":{\"temp\":296.26,\"feels_like\":296.4,\"temp_min\":292.8,\"temp_max\":297.64,\"pressure\":1019,\"humidity\":68,\"sea_level\":1019,\"grnd_level\":936},\"visibility\":10000,\"wind\":{\"speed\":2.06,\"deg\":28,\"gust\":1.45},\"clouds\":{\"all\":64},\"dt\":1662291900,\"sys\":{\"type\":2,\"id\":2004688,\"country\":\"IT\",\"sunrise\":1662266533,\"sunset\":1662313710},\"timezone\":7200,\"id\":3163858,\"name\":\"Zocca\",\"cod\":200}";
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(jsonResponse),
                StatusCode = System.Net.HttpStatusCode.OK
            };
            restServiceFake.Setup(s => s.GetHttpResponseMessage(
                It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(response);
            var sut = CreateOpenWeatherService();

            // Act
            var result = await sut.GetWeatherResponse(
                new WeatherRequestParameters("test city", "test language", isMetric: true),
                CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccessful);
                Assert.That(result.WeatherDetails.CityName, Is.EqualTo("Zocca"));
                Assert.That(result.WeatherDetails.Description, Is.EqualTo("broken clouds"));
                Assert.That(result.WeatherDetails.Temperature, Is.EqualTo(296.26));
                Assert.That(result.WeatherDetails.TemperatureFeelsLike, Is.EqualTo(296.4));
                Assert.That(result.WeatherDetails.TemperatureMin, Is.EqualTo(292.8));
                Assert.That(result.WeatherDetails.TemperatureMax, Is.EqualTo(297.64));
            });
        }

        [Test]
        public async Task WeatherService_gets_response_and_no_weather_details_on_unsuccessful_request()
        {
            // Arrange
            const string jsonResponse = "{\"cod\":\"404\",\"message\":\"city not found\"}";
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(jsonResponse),
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
            restServiceFake.Setup(s => s.GetHttpResponseMessage(
                It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(response);
            var sut = CreateOpenWeatherService();

            // Act
            var result = await sut.GetWeatherResponse(
                new WeatherRequestParameters("test city", "test language", isMetric: true), 
                CancellationToken.None);

            Assert.Multiple(() =>
            {
                Assert.That(!result.IsSuccessful);
                Assert.That(result.WeatherDetails, Is.EqualTo(null));
                Assert.That(result.ReasonPhrase, Is.EqualTo("Not Found: city not found"));
            });
        }

        [Test]
        public void WeatherService_throws_JsonReaderException_when_receives_incorrectly_formed_data()
        {
            // Arrange
            const string jsonResponse = "Not a JSON string";
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(jsonResponse),
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
            restServiceFake.Setup(s => s.GetHttpResponseMessage(
                It.IsAny<string>(), CancellationToken.None)).ReturnsAsync(response);
            var sut = CreateOpenWeatherService();

            Assert.ThrowsAsync<JsonReaderException>(
                async () => await sut.GetWeatherResponse(
                    new WeatherRequestParameters("test city", "test language", isMetric: true),
                    CancellationToken.None));
        }

        private OpenWeatherServiceWeb CreateOpenWeatherService() =>
            new(restServiceFake.Object, loggerFake.Object);

    }
}