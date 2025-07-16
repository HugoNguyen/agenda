using BookingGuru.Common.Application.Messaging;

namespace BookingGuru.Modules.Mocks.Application.FirstFeats.CreateFirstEntity;

public sealed record CreateFirstEntityCommand(string Field1, string? Field1Nullable, DateTime? Field2Utc)
    : ICommand<Guid>;