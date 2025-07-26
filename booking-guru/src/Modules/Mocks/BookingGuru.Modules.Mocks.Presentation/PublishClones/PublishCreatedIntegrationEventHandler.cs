using BookingGuru.Common.Application.EventBus;
using BookingGuru.Common.Application.Exceptions;
using BookingGuru.Common.Domain;
using BookingGuru.Modules.Mock2s.IntegrationEvents;
using BookingGuru.Modules.Mocks.Application.PublishClones.CreatePublishClone;
using MediatR;

namespace BookingGuru.Modules.Mocks.Presentation.PublishClones;

internal sealed class PublishCreatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<PublishCreatedIntegrationEvent>
{
    public override async Task Handle(PublishCreatedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new CreatePublishCloneCommand(
            integrationEvent.PublishId,
            integrationEvent.Name,
            integrationEvent.PublishDateUtc), cancellationToken);

        if (result.IsFailure)
        {
            throw new BookingGuruException(nameof(CreatePublishCloneCommand), result.Error);
        }
    }
}