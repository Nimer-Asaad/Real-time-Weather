using Weather.App.Parsing;
using FluentAssertions;

namespace Weather.App.Tests.Parsers;

public class JsonWeatherParserTests
{
    [Fact]
    public void CanParse_ReturnsTrue_WhenInputStartsWithBrace()
    {
        var p = new JsonWeatherParser();
        p.CanParse("{ \"x\": 1 }").Should().BeTrue();
    }

    [Fact]
    public void TryParse_ReturnsTrue_AndFillsData_WhenJsonIsValid()
    {
        var p = new JsonWeatherParser();
        var input = TestHelpers.ValidJson("Zarqa", 27.5, 65);

        var ok = p.TryParse(input, out var data);

        ok.Should().BeTrue();
        data.Should().NotBeNull();
        data!.Location.Should().Be("Zarqa");
        data.Temperature.Should().Be(27.5);
        data.Humidity.Should().Be(65);
    }

    [Fact]
    public void TryParse_ReturnsFalse_WhenJsonIsInvalid()
    {
        var p = new JsonWeatherParser();
        var input = "{ invalid json ...";

        var ok = p.TryParse(input, out var data);

        ok.Should().BeFalse();
        data.Should().BeNull();
    }
}
