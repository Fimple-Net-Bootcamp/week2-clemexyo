namespace week2.Models;

public class Planet
{
    public Guid Id { get; }
    public string Name { get; }
    public DateTime CreatedAt { get; }
    public Weather Weather { get; set; }

    public Planet(Guid id, string name)
    {
        Id = id;
        Name = name;
        CreatedAt = DateTime.Now;
    }
}
