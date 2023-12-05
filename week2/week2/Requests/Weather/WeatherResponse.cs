namespace week2.Requests.Weather;

public record WeatherResponse
(
    Guid id,
    string Name,
    string Description
);
