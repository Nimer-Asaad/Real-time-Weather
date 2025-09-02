using Weather.App.Models;


namespace Weather.App.Parsing;


public interface IWeatherParser
{
    bool CanParse(string input);
    bool TryParse(string input, out WeatherData? data);
}