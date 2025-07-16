
using BookingGuru.Common.Application.Messaging;

namespace BookingGuru.Modules.Mocks.Application.FirstFeats.GetFirstEntity;

public sealed record class GetFirstEntityQuery(Guid Id) : IQuery<FirstEntityResponse>;