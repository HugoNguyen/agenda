using BookingGuru.Common.Application.EventBus;

namespace BookingGuru.Modules.Mock2s.IntegrationEvents;

public sealed class PublishCreatedIntegrationEvent : IntegrationEvent
{
    public PublishCreatedIntegrationEvent(Guid id, DateTime occurredOnUtc, Guid publishId, string name, DateTimeOffset publishDateUtc) : base(id, occurredOnUtc)
    {
        PublishId = publishId;
        Name = name;
        PublishDateUtc = publishDateUtc;
    }

    public Guid PublishId { get; init; }

    public string Name { get; init; }

    public DateTimeOffset PublishDateUtc { get; init; }
}
