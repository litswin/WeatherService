using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Options;
using WeatherService.Api.Enums;
using WeatherService.Api.Options;
using WeatherService.Api.Providers;

namespace WeatherService.Api.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherProvider _weatherProvider;
        private readonly EventSystemOptions _eventSystemOptions;

        public WeatherService(IWeatherProvider weatherProvider, IOptions<EventSystemOptions> eventSystemOptions)
        {
            _weatherProvider = weatherProvider;
            _eventSystemOptions = eventSystemOptions.Value;
        }

        public async Task<double> GetTemperatureAsync(string cityName, TemperatureUnit temperatureUnit, CancellationToken cancellationToken)
        {
            var temperatureInKelvins = await _weatherProvider.GetTemperatureAsync(cityName, cancellationToken);
            var result = ConvertTemperatureFromKelvin(temperatureInKelvins, temperatureUnit);
            await SendTemperatureEventAsync(result);
            return result;
        }

        public async Task<double> GetTemperatureByCoordinatesAsync(double longitude, double latitude, TemperatureUnit temperatureUnit, CancellationToken cancellationToken)
        {
            var temperatureInKelvins = await _weatherProvider.GetTemperatureByCoordinatesAsync(longitude, latitude, cancellationToken);
            var result = ConvertTemperatureFromKelvin(temperatureInKelvins, temperatureUnit);
            await SendTemperatureEventAsync(result);
            return result;
        }

        private async Task SendTemperatureEventAsync(double temperature)
        {
            //ToDo: abstract to the service
            var producerClient = new EventHubProducerClient(_eventSystemOptions.ConnectionString, _eventSystemOptions.EventHubName);
            var events = new List<EventData> {new EventData(temperature.ToString(CultureInfo.InvariantCulture))};

            await producerClient.SendAsync(events, CancellationToken.None);
        }

        private double ConvertTemperatureFromKelvin(double value, TemperatureUnit temperatureUnit)
        {
            switch (temperatureUnit)
            {
                case TemperatureUnit.Celsius: return TemperatureConverter.FromKelvinToCelsius(value);
                case TemperatureUnit.Fahrenheit: return TemperatureConverter.FromKelvinToFahrenheit(value);
                default: throw new ArgumentOutOfRangeException(nameof(temperatureUnit));
            }
        }
    }
}
