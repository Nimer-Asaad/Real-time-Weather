using Weather.App.Parsing;
using FluentAssertions;

namespace Weather.App.Tests.Parsers;

public class ParserRegistryTests
{
    [Fact]
    public void Find_ReturnsJsonParser_ForJsonInput()
    {
        var reg = new ParserRegistry()
            .Register(new JsonWeatherParser())
            .Register(new XmlWeatherParser());

        var parser = reg.Find(TestHelpers.ValidJson());
        parser.Should().BeOfType<JsonWeatherParser>();
    }

    [Fact]
    public void Find_ReturnsXmlParser_ForXmlInput()
    {
        var reg = new ParserRegistry()
            .Register(new JsonWeatherParser())
            .Register(new XmlWeatherParser());

        var parser = reg.Find(TestHelpers.ValidXml());
        parser.Should().BeOfType<XmlWeatherParser>();
    }

    [Fact]
    public void Find_ReturnsNull_ForUnrecognizedFormat()
    {
        var reg = new ParserRegistry()
            .Register(new JsonWeatherParser())
            .Register(new XmlWeatherParser());

        var parser = reg.Find("?? not json nor xml ??");
        parser.Should().BeNull();
    }
}
