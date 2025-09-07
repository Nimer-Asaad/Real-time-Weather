using System.IO;
using Weather.App.Bots;
using FluentAssertions;

namespace Weather.App.Tests.Bots;

public class SunBotTests
{
    [Fact]
    public void OnWeather_PrintsMessage_WhenTempAboveThreshold_AndEnabled()
    {
        var bot = new SunBot(enabled: true, temperatureThreshold: 30, message: "Stay hydrated!");
        using var sw = new StringWriter(); Console.SetOut(sw);

        bot.OnWeather(new("Aqaba", 35, 20));

        var output = sw.ToString();
        output.Should().Contain("SunBot activated!");
        output.Should().Contain("Stay hydrated!");
    }

    [Fact]
    public void OnWeather_DoesNothing_WhenNotHotEnough()
    {
        var bot = new SunBot(true, 40, "Hi");
        using var sw = new StringWriter(); Console.SetOut(sw);

        bot.OnWeather(new("Amman", 22, 50));

        sw.ToString().Should().BeEmpty();
    }
}
