using System.Threading;
using System.Threading.Tasks;
using WeatherService.Api.Enums;

namespace WeatherService.Api.Services
{
    public interface IWeatherService
    {
        Task<double> GetTemperatureAsync(string cityName, TemperatureUnit temperatureUnit, CancellationToken cancellationToken);

        Task<double> GetTemperatureByCoordinatesAsync(double longitude, double latitude, TemperatureUnit temperatureUnit, CancellationToken cancellationToken);
    }
}