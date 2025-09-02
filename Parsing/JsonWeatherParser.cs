using System.Text.Json;
using Weather.App.Models;


namespace Weather.App.Parsing;


public class JsonWeatherParser : IWeatherParser
{
    public bool CanParse(string input)
    => input.TrimStart().StartsWith("{");


    public bool TryParse(string input, out WeatherData? data)
    {
        try
        {
            using var doc = JsonDocument.Parse(input);
            var root = doc.RootElement;
            var loc = root.GetProperty("Location").GetString() ?? "";
            var temp = root.GetProperty("Temperature").GetDouble();
            var hum = root.GetProperty("Humidity").GetDouble();
            data = new WeatherData(loc, temp, hum);
            return true;
        }
        catch
        {
            data = null;
            return false;
        }
    }
}