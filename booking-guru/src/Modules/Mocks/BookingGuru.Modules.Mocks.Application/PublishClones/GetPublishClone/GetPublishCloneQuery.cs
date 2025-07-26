using BookingGuru.Common.Application.Messaging;

namespace BookingGuru.Modules.Mocks.Application.Publishes.GetPublishClone;

public sealed record class GetPublishCloneQuery(Guid Id) : IQuery<PublishCloneResponse>;