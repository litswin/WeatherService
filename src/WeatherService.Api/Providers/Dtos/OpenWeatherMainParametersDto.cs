using System.Text.Json.Serialization;

namespace WeatherService.Api.Providers.Dtos
{
    public class OpenWeatherMainParametersDto
    {
        [JsonPropertyName("temp")]
        public double Temperature { get; set; }
    }
}