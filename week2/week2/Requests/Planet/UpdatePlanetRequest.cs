namespace week2.Requests.Planet;
using Models;


public record UpdatePlanetRequest
(
    string Name,
    Weather Weather
);
