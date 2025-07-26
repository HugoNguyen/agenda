namespace BookingGuru.Modules.Mocks.Application.Publishes.GetPublishClone;

public sealed record PublishCloneResponse(Guid Id, string Name, DateTimeOffset PublishDateUtc);