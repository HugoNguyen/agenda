namespace BookingGuru.Common.Domain.Entities;

public interface IDomainEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}