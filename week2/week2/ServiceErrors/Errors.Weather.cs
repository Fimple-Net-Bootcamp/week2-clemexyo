using ErrorOr;

namespace week2.ServiceErrors;

public static class Errors
{
    public static class Weather
    {
        public static Error NotFound => Error.NotFound(
            code: "Weather.NotFound",
            description: "Weather is not found.");
    }
}
