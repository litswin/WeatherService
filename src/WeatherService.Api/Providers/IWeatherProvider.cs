using System.Threading;
using System.Threading.Tasks;

namespace WeatherService.Api.Providers
{
    public interface IWeatherProvider
    {
        /// <summary>
        /// Get temperature from third party weather provider.
        /// </summary>
        /// <param name="cityName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Temperature, K</returns>
        Task<double> GetTemperatureAsync(string cityName, CancellationToken cancellationToken);

        Task<double> GetTemperatureByCoordinatesAsync(double longitude, double latitude, CancellationToken cancellationToken);
    }
}