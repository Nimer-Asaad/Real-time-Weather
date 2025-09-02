using System.Xml.Linq;
using Weather.App.Models;


namespace Weather.App.Parsing;


public class XmlWeatherParser : IWeatherParser
{
    public bool CanParse(string input)
    => input.TrimStart().StartsWith("<");


    public bool TryParse(string input, out WeatherData? data)
    {
        try
        {
            var x = XDocument.Parse(input);
            var root = x.Root!; // <WeatherData>
            var loc = root.Element("Location")?.Value ?? "";
            var temp = double.Parse(root.Element("Temperature")!.Value);
            var hum = double.Parse(root.Element("Humidity")!.Value);
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