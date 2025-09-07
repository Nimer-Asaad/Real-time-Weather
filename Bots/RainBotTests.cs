using System.IO;
using Weather.App.Bots;
using Weather.App.Tests;
using FluentAssertions;

namespace Weather.App.Tests.Bots;

public class RainBotTests
{
    [Fact]
    public void OnWeather_PrintsMessage_WhenHumidityAboveThreshold_AndEnabled()
    {
        var bot = new RainBot(enabled: true, humidityThreshold: 70, message: "Bring umbrella!");
        var data = TestHelpers.Data(hum: 80);

        using var sw = new StringWriter();
        Console.SetOut(sw);

        bot.OnWeather(data);

        var output = sw.ToString();
        output.Should().Contain("RainBot activated!");
        output.Should().Contain("Bring umbrella!");
    }

    [Fact]
    public void OnWeather_DoesNothing_WhenBelowThreshold_OrDisabled()
    {
        var bot = new RainBot(enabled: true, humidityThreshold: 90, message: "Hi");
        var data = TestHelpers.Data(hum: 60);

        using var sw = new StringWriter();
        Console.SetOut(sw);

        bot.OnWeather(data);

        sw.ToString().Should().BeEmpty();
    }

    [Fact]
    public void OnWeather_DoesNothing_WhenDisabled()
    {
        var bot = new RainBot(enabled: false, humidityThreshold: 50, message: "Hi");
        var data = TestHelpers.Data(hum: 100);

        using var sw = new StringWriter();
        Console.SetOut(sw);

        bot.OnWeather(data);

        sw.ToString().Should().BeEmpty();
    }
}
