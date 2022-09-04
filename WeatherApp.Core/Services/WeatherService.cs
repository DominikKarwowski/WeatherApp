﻿using DjK.WeatherApp.Core.Models;
using DjK.WeatherApp.Core.Services.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services
{
    public class OpenWeatherService : IWeatherService
    {
        private readonly IRestService _restService;

        /// <summary>
        /// Creates OpenWeatherService instance.
        /// </summary>
        /// <param name="restService">IRestService implementation.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public OpenWeatherService(IRestService restService)
        {
            _restService = restService ?? throw new ArgumentNullException(nameof(restService));
        }

        /// <summary>
        /// Sends GET request to Open Weather API to retrieve the weather data for the specified city.
        /// </summary>
        /// <param name="cityName">City for which the weather data are requested.</param>
        /// <returns>WeatherResponse object. If request was not successful, WetherDetails object is null.</returns>
        public async Task<WeatherResponse> GetWeatherResponseForLocation(string cityName)
        {
            var uri = BuildRequestUri(cityName);
            var response = await _restService.GetHttpResponseMessage(uri);
            var reasonPhrase = response.ReasonPhrase;
            var content = await response.Content.ReadAsStringAsync();

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
                    TemperatureMax = Convert.ToDouble(weatherData["main"]["temp_max"].ToString())
                };

        private TResult ParseResponseContent<TResult>(string message, Func<JObject, TResult> contentParser)
        {
            try
            {
                var content = JObject.Parse(message);
                return contentParser(content);
            }
            catch (JsonReaderException)
            {
                // TODO: add logging
                throw;
            }
            catch (Exception)
            {
                // TODO: add logging
                throw;
            }
        }

        private string BuildRequestUri(string cityName)
        {
            // TODO: customize request based on CultureInfo
            // CultureInfo cultureInfo = CultureInfo.GetCultureInfo();

            return $@"{Constants.Constants.OpenWeatherMapEndpoint}?q={cityName}&appid={Constants.Constants.OpenWeatherMapAPIKey}";

        }
    }
}
