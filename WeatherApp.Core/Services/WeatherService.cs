using DjK.WeatherApp.Core.Models;
using DjK.WeatherApp.Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DjK.WeatherApp.Core.Services
{
    public class OpenWeatherService : IWeatherService
    {
        private readonly IRestService _restService;

        public OpenWeatherService(IRestService restService)
        {
            _restService = restService ?? throw new ArgumentNullException(nameof(restService));
        }

        public async Task<WeatherResponse> GetWeatherResponseForLocation(string cityName)
        {
            var uri = BuildRequestUri(cityName);
            var response = await _restService.GetHttpResponseMessage(uri);
            var reasonPhrase = response.ReasonPhrase;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var weatherDetails = ParseResponseContent(content);
                return new WeatherResponse(weatherDetails, reasonPhrase, isSuccessful: true);
            }
            else
            {
                return new WeatherResponse(weatherDetails: default, reasonPhrase, isSuccessful: false);
            }
        }

        private WeatherDetails ParseResponseContent(string content)
        {
            throw new NotImplementedException();
        }

        private string BuildRequestUri(string cityName)
        {
            throw new NotImplementedException();
        }
    }
}
