using DjK.WeatherApp.Core.Models;
using DjK.WeatherApp.Core.Services.Abstractions;
using Microsoft.Extensions.Logging;
using MvvmCross.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services
{
    /// <summary>
    /// Weather service implementation for Open Weather API.
    /// </summary>
    public class OpenWeatherServiceWeb : IWeatherServiceWeb, IDisposable
    {
        private readonly IRestServiceWeb _restService;
        private readonly ILogger<OpenWeatherServiceWeb> _logger;

        /// <summary>
        /// Creates OpenWeatherServiceWeb instance.
        /// </summary>
        /// <param name="restService">IRestService implementation.</param>
        /// <param name="logger">Logger implementation.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public OpenWeatherServiceWeb(IRestServiceWeb restService, ILogger<OpenWeatherServiceWeb> logger)
        {
            _restService = restService ?? throw new ArgumentNullException(nameof(restService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Sends GET request to Open Weather API to retrieve the weather data for the specified city.
        /// </summary>
        /// <param name="parameters">Request parameters as described in WeatherRequestParameters object.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>WeatherResponse object. If request was not successful, WeatherDetails object is null.</returns>
        public async Task<WeatherResponse> GetWeatherResponse(
            WeatherRequestParameters parameters, CancellationToken cancellationToken)
        {
            try
            {
                var uri = BuildWeatherRequestUri(parameters);
                var response = await _restService.GetHttpResponseMessage(uri, cancellationToken)
                    .ConfigureAwait(false);
                var reasonPhrase = response.ReasonPhrase;
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                cancellationToken.ThrowIfCancellationRequested();

                if (response.IsSuccessStatusCode)
                {
                    var weatherDetails = ParseResponseContent(content, WeatherDataParser);
                    return new WeatherResponse(weatherDetails, reasonPhrase, isSuccessful: true);
                }
                else
                {
                    var message = ParseResponseContent(content, UnsuccessfulResponseParser);
                    return new WeatherResponse(
                        weatherDetails: default,
                        reasonPhrase: $"{reasonPhrase}: {message}",
                        isSuccessful: false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        private Func<JObject, string> UnsuccessfulResponseParser =>
            message => message["message"].ToString();

        private Func<JObject, WeatherDetails> WeatherDataParser =>
            weatherData => new WeatherDetails()
                {
                    CityName = weatherData["name"].ToString(),
                    Description = weatherData["weather"][0]["description"].ToString(),
                    Temperature = Convert.ToDouble(weatherData["main"]["temp"].ToString()),
                    TemperatureFeelsLike = Convert.ToDouble(weatherData["main"]["feels_like"].ToString()),
                    TemperatureMin = Convert.ToDouble(weatherData["main"]["temp_min"].ToString()),
                    TemperatureMax = Convert.ToDouble(weatherData["main"]["temp_max"].ToString()),
                };

        private TResult ParseResponseContent<TResult>(string message, Func<JObject, TResult> contentParser)
        {
            try
            {
                var content = JObject.Parse(message);
                return contentParser(content);
            }
            catch (JsonReaderException ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        private string BuildWeatherRequestUri(WeatherRequestParameters parameters)
        {
            var units = parameters.IsMetric ? "metric" : "imperial";
            return $@"{Constants.Constants.OpenWeatherMapEndpoint}?q={parameters.CityName}&lang={parameters.Language}&appid={Constants.Constants.OpenWeatherMapAPIKey}&units={units}";

        }

        public void Dispose()
        {
            _restService.DisposeIfDisposable();
        }
    }
}
