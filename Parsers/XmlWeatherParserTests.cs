using Weather.App.Parsing;
using FluentAssertions;

namespace Weather.App.Tests.Parsers;

public class XmlWeatherParserTests
{
    [Fact]
    public void CanParse_ReturnsTrue_WhenInputStartsWithAngle()
    {
        var p = new XmlWeatherParser();
        p.CanParse("<WeatherData></WeatherData>").Should().BeTrue();
    }

    [Fact]
    public void TryParse_ReturnsTrue_AndFillsData_WhenXmlIsValid()
    {
        var p = new XmlWeatherParser();
        var input = TestHelpers.ValidXml("Irbid", 12.3, 90);

        var ok = p.TryParse(input, out var data);

        ok.Should().BeTrue();
        data.Should().NotBeNull();
        data!.Location.Should().Be("Irbid");
        data.Temperature.Should().Be(12.3);
        data.Humidity.Should().Be(90);
    }

    [Fact]
    public void TryParse_ReturnsFalse_WhenXmlIsInvalid()
    {
        var p = new XmlWeatherParser();
        var input = "<WeatherData><Location>Amman</Location>"; // ناقص إغلاق

        var ok = p.TryParse(input, out var data);

        ok.Should().BeFalse();
        data.Should().BeNull();
    }
}
