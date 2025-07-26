namespace BookingGuru.Common.Domain.Entities;

public interface IDomainEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents  { get; }

    void ClearDomainEvents();
}

public abstract class DomainEntity<TPrimaryKey> : Entity<TPrimaryKey>, IDomainEntity where TPrimaryKey : struct
{
    protected DomainEntity() { }

    protected DomainEntity(TPrimaryKey id) : base(id) { }

    /// <summary>
    /// Contain the domain events that raised on this entity
    /// </summary>
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyList<IDomainEvent> DomainEvents => [.. _domainEvents];

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
