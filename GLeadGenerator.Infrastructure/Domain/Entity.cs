namespace GLeadGenerator.Infrastructure.Domain;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; } = default!;
}

public class Entity : Entity<int>
{
}

