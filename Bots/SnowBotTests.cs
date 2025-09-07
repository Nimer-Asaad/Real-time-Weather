using System.IO;
using Weather.App.Bots;
using FluentAssertions;

namespace Weather.App.Tests.Bots;

public class SnowBotTests
{
    [Fact]
    public void OnWeather_PrintsMessage_WhenTempBelowThreshold_AndEnabled()
    {
        var bot = new SnowBot(true, temperatureThreshold: 5, message: "It might snow!");
        using var sw = new StringWriter(); Console.SetOut(sw);

        bot.OnWeather(new("Ajloun", 0, 60));

        var output = sw.ToString();
        output.Should().Contain("SnowBot activated!");
        output.Should().Contain("It might snow!");
    }

    [Fact]
    public void OnWeather_DoesNothing_WhenNotColdEnough()
    {
        var bot = new SnowBot(true, 0, "Hi");
        using var sw = new StringWriter(); Console.SetOut(sw);

        bot.OnWeather(new("Amman", 10, 40));

        sw.ToString().Should().BeEmpty();
    }
}
