namespace BookingGuru.Modules.Mock2s.Application.Publishes.GetPublish;

public sealed record PublishResponse(Guid Id, string Name, DateTimeOffset PublishDateUtc);