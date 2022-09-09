namespace Core.Persistence.Repositories;

public class Entity
{
    public int Id { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    public Entity()
    {
    }
    public Entity(int id, DateTime creationTime) : this()
    {
        Id = id;
        CreationTime = creationTime;
    }

    public Entity(int id) : this()
    {
        Id = id;
    }
}