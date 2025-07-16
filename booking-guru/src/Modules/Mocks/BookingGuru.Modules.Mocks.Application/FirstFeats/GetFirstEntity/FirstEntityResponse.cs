namespace BookingGuru.Modules.Mocks.Application.FirstFeats.GetFirstEntity;

public sealed record FirstEntityResponse(Guid Id, string Field1, string? Field1Nullable, DateTimeOffset? Field2Utc);