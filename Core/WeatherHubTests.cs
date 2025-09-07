using Weather.App.Core;
using Weather.App.Bots;
using Weather.App.Models;
using FluentAssertions;

namespace Weather.App.Tests.Core;

file class FakeBot : IWeatherBot
{
    public string Name => "Fake";
    public bool IsEnabled => true;
    public List<WeatherData> Received { get; } = new();

    public void OnWeather(WeatherData data) => Received.Add(data);
}

public class WeatherHubTests
{
    [Fact]
    public void Publish_InvokesOnWeather_OnAllSubscribers()
    {
        var hub = new WeatherHub();
        var b1 = new FakeBot();
        var b2 = new FakeBot();

        hub.Subscribe(b1);
        hub.Subscribe(b2);

        var d = new WeatherData("Salt", 21, 50);
        hub.Publish(d);

        b1.Received.Should().ContainSingle().Which.Should().Be(d);
        b2.Received.Should().ContainSingle().Which.Should().Be(d);
    }
}
