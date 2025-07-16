using BookingGuru.Common.Application.Messaging;

namespace BookingGuru.Modules.Mock2s.Application.Publishes.CreatePublish;

public sealed record CreatePublishCommand(string Name, DateTimeOffset PublishDateUtc)
    : ICommand<Guid>;