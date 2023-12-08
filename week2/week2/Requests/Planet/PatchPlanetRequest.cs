namespace week2.Requests.Planet;

public record PatchPlanetRequest
(
    Guid id,
    string Name
);
