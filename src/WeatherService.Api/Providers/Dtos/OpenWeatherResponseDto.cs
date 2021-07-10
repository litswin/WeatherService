using System.Text.Json.Serialization;

namespace WeatherService.Api.Providers.Dtos
{
    public class OpenWeatherResponseDto
    {
        [JsonPropertyName("main")]
        public OpenWeatherMainParametersDto MainParametersDto { get; set; }
    }
}
