using ErrorOr;
using week2.Models;

namespace week2.Services.Weathers;

public interface IWeatherService
{
    ErrorOr<Created> CreateWeather(Weather weather);
    ErrorOr<Deleted> DeleteWeather(Guid id);
    ErrorOr<Weather> GetWeather(Guid id);
    ErrorOr<Updated> UpdateWeather(Weather weather);
    ErrorOr<Weather> GetRandomWeather();

    ErrorOr<Dictionary<Guid, Weather>> GetWeathersDescSorted();
    ErrorOr<Dictionary<Guid, Weather>> GetWeathersAscSorted();
}
