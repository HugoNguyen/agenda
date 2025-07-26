namespace BookingGuru.Common.Domain.Entities;

public abstract class Entity : IEntity
{
    public abstract object?[] GetKeys();
}

public abstract class Entity<TPrimaryKey> : Entity, IEntity<TPrimaryKey> where TPrimaryKey : struct
{
    /// <summary>
    /// Unique identifier for this entity.
    /// </summary>
    public virtual TPrimaryKey Id { get; protected set; } = default!;

    protected Entity()
    {

    }

    protected Entity(TPrimaryKey id)
    {
        Id = id;
    }

    public override object?[] GetKeys()
    {
        return new object?[] { Id };
    }

    public override string ToString()
    {
        return $"[{GetType().Name} {Id}]";
    }
}

