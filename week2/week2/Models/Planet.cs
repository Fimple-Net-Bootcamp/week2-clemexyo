namespace week2.Models;

public class Planet
{
    public Guid Id { get; }
    public string Name { get; set;  }
    public DateTime CreatedAt { get; }
    public Weather Weather { get; set; }

    public Planet(Guid id, string name)
    {
        Id = id;
        Name = name;
        CreatedAt = DateTime.Now;
    }

    public Planet(Guid id, string name, Weather weather)
    {
        Id = id;
        Name = name;
        Weather = weather;
        CreatedAt = DateTime.Now;
    }
}
