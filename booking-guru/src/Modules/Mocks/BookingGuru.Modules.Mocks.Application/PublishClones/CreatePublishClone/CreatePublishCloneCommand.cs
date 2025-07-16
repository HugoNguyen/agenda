using BookingGuru.Common.Application.Messaging;

namespace BookingGuru.Modules.Mocks.Application.PublishClones.CreatePublishClone;

public sealed record CreatePublishCloneCommand(Guid PublishId, string Name, DateTimeOffset PublishDateUtc)
    : ICommand<Guid>;