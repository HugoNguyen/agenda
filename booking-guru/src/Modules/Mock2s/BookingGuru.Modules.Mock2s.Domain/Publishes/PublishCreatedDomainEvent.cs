using BookingGuru.Common.Domain;

namespace BookingGuru.Modules.Mock2s.Domain.Publishes;

public sealed class PublishCreatedDomainEvent(Guid id) : DomainEvent
{
    public Guid PublishId { get; init; } = id;
}
