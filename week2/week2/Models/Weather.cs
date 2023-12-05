namespace week2.Models;

public class Weather
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }

    public Weather(Guid id, string name, string description)
    {
        //enforce something
        Id = id;
        Name = name;
        Description = description;
    }
}
