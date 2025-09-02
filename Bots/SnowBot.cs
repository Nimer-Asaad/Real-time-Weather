using Weather.App.Models;


namespace Weather.App.Bots;


public class SnowBot : IWeatherBot
{
    private readonly double _threshold;
    private readonly string _message;
    public string Name => nameof(SnowBot);
    public bool IsEnabled { get; }


    public SnowBot(bool enabled, double temperatureThreshold, string message)
    {
        IsEnabled = enabled;
        _threshold = temperatureThreshold;
        _message = message;
    }


    public void OnWeather(WeatherData data)
    {
        if (!IsEnabled) return;
        if (data.Temperature < _threshold)
        {
            Console.WriteLine("SnowBot activated!");
            Console.WriteLine($"SnowBot: \"{_message}\"");
        }
    }
}