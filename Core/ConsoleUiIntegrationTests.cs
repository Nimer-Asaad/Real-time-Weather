using System.IO;
using Weather.App.Core;
using Weather.App.Parsing;
using Weather.App.Bots;
using FluentAssertions;

namespace Weather.App.Tests.Core;

public class ConsoleUiIntegrationTests
{
    [Fact]
    public void Run_ParsesInput_AndActivatesBots_ThenExits()
    {
        // Arrange: registry + hub + bot + IO streams
        var registry = new ParserRegistry()
            .Register(new JsonWeatherParser())
            .Register(new XmlWeatherParser());

        var hub = new WeatherHub();
        var bot = new SunBot(true, temperatureThreshold: 25, message: "Hot!");
        hub.Subscribe(bot);

        var input =
            TestHelpers.ValidJson("Amman", 30, 40) + Environment.NewLine +
            Environment.NewLine +   // نهاية البلوك
            "exit" + Environment.NewLine;

        using var sr = new StringReader(input);
        using var sw = new StringWriter();
        Console.SetIn(sr);
        Console.SetOut(sw);

        // Act
        ConsoleUi.Run(registry, hub);

        // Assert: خرجت رسالة تفعيل البوت
        var output = sw.ToString();
        output.Should().Contain("SunBot activated!");
        output.Should().Contain("Hot!");
        output.Should().Contain("Type 'exit' alone to quit.");
    }
}
