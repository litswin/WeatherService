using System.Threading;
using System.Threading.Tasks;
using Moq;
using WeatherService.Api.Enums;
using WeatherService.Api.Providers;
using Xunit;

namespace WeatherService.Api.Tests
{
    public class WeatherServiceTests
    {
        private const string TestCityName = "TestCityName";
        private readonly Services.WeatherService _weatherService;

        public WeatherServiceTests()
        {
            var weatherProviderMock = new Mock<IWeatherProvider>();
            weatherProviderMock.Setup(x => x.GetTemperatureAsync(TestCityName, CancellationToken.None)).ReturnsAsync(100);

            _weatherService = new Services.WeatherService(weatherProviderMock.Object);
        }

        [Fact]
        public async Task GetTemperature_AsCelsius_CorrectResult()
        {
            // Act
            var result = await _weatherService.GetTemperatureAsync(TestCityName, TemperatureUnit.Celsius, CancellationToken.None);

            // Assert
            Assert.Equal(-173.15, result, 5);
        }

        [Fact]
        public async Task GetTemperature_AsFahrenheit_CorrectResult()
        {
            // Act
            var result = await _weatherService.GetTemperatureAsync(TestCityName, TemperatureUnit.Fahrenheit, CancellationToken.None);

            // Assert
            Assert.Equal(-279.67, result, 5);
        }
    }
}
