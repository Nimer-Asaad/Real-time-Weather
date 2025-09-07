using Weather.App.Models;

namespace Weather.App.Tests;

public static class TestHelpers
{
    public static string ValidJson(string loc = "Amman", double temp = 30, double hum = 80) =>
        $$"""
        {
          "Location": "{{loc}}",
          "Temperature": {{temp}},
          "Humidity": {{hum}}
        }
        """;

    public static string ValidXml(string loc = "Amman", double temp = 30, double hum = 80) =>
        $$"""
        <WeatherData>
          <Location>{{loc}}</Location>
          <Temperature>{{temp}}</Temperature>
          <Humidity>{{hum}}</Humidity>
        </WeatherData>
        """;

    public static WeatherData Data(string loc = "Amman", double temp = 30, double hum = 80)
        => new(loc, temp, hum);
}
