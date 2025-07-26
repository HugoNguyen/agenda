namespace BookingGuru.Common.Domain.Entities;

public interface IEntity
{
    /// <summary>
    /// Returns an array of ordered keys for this entity.
    /// </summary>
    /// <returns></returns>
    object?[] GetKeys();
}


public interface IEntity<out TPrimaryKey> : IEntity where TPrimaryKey : struct
{
    //
    // Summary:
    //     Unique identifier for this entity.
    TPrimaryKey Id { get; }
}

