using Weather.App.Models;


namespace Weather.App.Bots;


public class SunBot : IWeatherBot
{
    private readonly double _threshold;
    private readonly string _message;
    public string Name => nameof(SunBot);
    public bool IsEnabled { get; }


    public SunBot(bool enabled, double temperatureThreshold, string message)
    {
        IsEnabled = enabled;
        _threshold = temperatureThreshold;
        _message = message;
    }


    public void OnWeather(WeatherData data)
    {
        if (!IsEnabled) return;
        if (data.Temperature > _threshold)
        {
            Console.WriteLine("SunBot activated!");
            Console.WriteLine($"SunBot: \"{_message}\"");
        }
    }
}