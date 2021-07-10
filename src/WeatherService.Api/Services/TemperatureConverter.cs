namespace WeatherService.Api.Services
{
    public static class TemperatureConverter
    {
        public static double FromKelvinToCelsius(double value)
        {
            return value - 273.15;
        }

        public static double FromKelvinToFahrenheit(double value)
        {
            return FromKelvinToCelsius(value) * (9.0 / 5.0) + 32;
        }
    }
}
