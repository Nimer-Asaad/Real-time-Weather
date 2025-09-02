using Weather.App.Models;


namespace Weather.App.Bots;


public interface IWeatherBot
{
    string Name { get; }
    bool IsEnabled { get; }
    void OnWeather(WeatherData data);
}