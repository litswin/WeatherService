using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WeatherService.Api.Options;
using WeatherService.Api.Providers.Dtos;

namespace WeatherService.Api.Providers
{
    public class OpenWeatherProvider : IWeatherProvider
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherProviderOptions _providerOptions;

        public OpenWeatherProvider(HttpClient httpClient, IOptions<WeatherProviderOptions> providerOptions)
        {
            _httpClient = httpClient;
            _providerOptions = providerOptions.Value;
        }

        public async Task<double> GetTemperatureAsync(string cityName, CancellationToken cancellationToken)
        {
            var url = $"{_httpClient.BaseAddress}?q={cityName}&appid={_providerOptions.ApiKey}";

            var response = await _httpClient.GetAsync(url, cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("City was not found");
            }

            return await GetTemperatureAsync(response, cancellationToken);
        }

        public async Task<double> GetTemperatureByCoordinatesAsync(double longitude, double latitude, CancellationToken cancellationToken)
        {
            var url = $"{_httpClient.BaseAddress}?lat={latitude}&lon={longitude}&appid={_providerOptions.ApiKey}";

            var response = await _httpClient.GetAsync(url, cancellationToken);
            return await GetTemperatureAsync(response, cancellationToken);
        }

        private static async Task<double> GetTemperatureAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            var streamTask = response.Content.ReadAsStreamAsync();
            var responseDto = await JsonSerializer.DeserializeAsync<OpenWeatherResponseDto>(await streamTask, null, cancellationToken);
            return responseDto.MainParametersDto.Temperature;
        }
    }
}
