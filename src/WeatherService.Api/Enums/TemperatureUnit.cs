using System.Text.Json.Serialization;

namespace WeatherService.Api.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TemperatureUnit
    {
        Celsius,
        Fahrenheit,
    }
}
