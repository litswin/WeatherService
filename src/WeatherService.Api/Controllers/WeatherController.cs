using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherService.Api.Enums;
using WeatherService.Api.Providers;
using WeatherService.Api.Services;

namespace WeatherService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<ActionResult<double>> Get(
            [Required][StringLength(100, MinimumLength = 1)]string cityName, 
            [Required]TemperatureUnit? temperatureUnit, 
            CancellationToken cancellationToken)
        {
            try
            {
                // ToDo: return not only double but also, identified city, temperatureUnit?
                return Ok(await _weatherService.GetTemperatureAsync(cityName, temperatureUnit.Value, cancellationToken));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("byCoordinates")]
        public async Task<ActionResult<double>> Get(
            [Required] double? latitude,
            [Required] double? longitude,
            [Required] TemperatureUnit? temperatureUnit,
            CancellationToken cancellationToken)
        {
            return Ok(await _weatherService.GetTemperatureByCoordinatesAsync(latitude.Value, longitude.Value, temperatureUnit.Value, cancellationToken));
        }
    }
}
