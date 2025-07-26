using BookingGuru.Common.Application.EventBus;
using BookingGuru.Common.Application.Exceptions;
using BookingGuru.Common.Application.Messaging;
using BookingGuru.Common.Domain;
using BookingGuru.Modules.Mock2s.Application.Publishes.GetPublish;
using BookingGuru.Modules.Mock2s.Domain.Publishes;
using BookingGuru.Modules.Mock2s.IntegrationEvents;
using MediatR;

namespace BookingGuru.Modules.Mock2s.Application.Publishes.CreatePublish;

internal sealed class PublishCreatedDomainEventHandler(ISender sender, IEventBus bus)
    : DomainEventHandler<PublishCreatedDomainEvent>
{
    public override async Task Handle(PublishCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Result<PublishResponse> result = await sender.Send(
            new GetPublishQuery(domainEvent.PublishId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new BookingGuruException(nameof(GetPublishQuery), result.Error);
        }

        await bus.PublishAsync(
            new PublishCreatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Name,
                result.Value.PublishDateUtc),
            cancellationToken);
    }
}
