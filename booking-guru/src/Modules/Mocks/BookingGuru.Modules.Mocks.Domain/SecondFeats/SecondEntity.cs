using BookingGuru.Common.Domain.Entities.Auditing;

namespace BookingGuru.Modules.Mocks.Domain.SecondFeats;

public sealed class SecondEntity : FullAuditedEntity<Guid, User, Guid>
{
    private SecondEntity() { }
    public required string Field1 { get; set; }
}