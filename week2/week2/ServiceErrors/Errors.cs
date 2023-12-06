namespace week2.ServiceErrors;
using ErrorOr;


public static class Errors
{
    public static class Weather
    {
        public static Error NotFound => Error.NotFound(
            code: "Weather.NotFound",
            description: "Weather is not found.");
    }
    public static class Planet
    {
        public static Error NotFound => Error.NotFound(
            code: "Planet.NotFound",
            description: "Planet is not found.");
    }
}
