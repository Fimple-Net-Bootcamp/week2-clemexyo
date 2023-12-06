namespace week2.Requests.Planet;
using Models;

public record PlanetResponse
(
    Guid id,
    string Name,
    Weather Weather
);
