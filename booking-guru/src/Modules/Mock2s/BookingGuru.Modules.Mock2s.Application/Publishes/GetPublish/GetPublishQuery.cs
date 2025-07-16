using BookingGuru.Common.Application.Messaging;

namespace BookingGuru.Modules.Mock2s.Application.Publishes.GetPublish;

public sealed record class GetPublishQuery(Guid Id) : IQuery<PublishResponse>;