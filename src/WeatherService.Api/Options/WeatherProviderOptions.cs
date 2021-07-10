using System;

namespace WeatherService.Api.Options
{
    public class WeatherProviderOptions
    {
        public Uri Url { get; set; }

        public string ApiKey { get; set; }
    }
}
