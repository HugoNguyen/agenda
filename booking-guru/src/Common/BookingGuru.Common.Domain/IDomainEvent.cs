using MediatR;

namespace BookingGuru.Common.Domain;

public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}
