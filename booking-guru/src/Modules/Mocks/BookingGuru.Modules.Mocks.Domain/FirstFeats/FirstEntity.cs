using BookingGuru.Common.Domain.Entities;

namespace BookingGuru.Modules.Mocks.Domain.FirstFeats;

public sealed class FirstEntity : Entity<Guid>
{
    private FirstEntity() { }

    public string Field1 { get; private set; }
    public string? Field1Nullable { get; private set; }

    public DateTimeOffset? Field2Utc { get; private set; }

    public static FirstEntity Create(string field1, string? field1Nullable, DateTime? field2Utc)
    {
        var obj = new FirstEntity
        {
            Field1 = field1,
            Field1Nullable = field1Nullable,
            Field2Utc = field2Utc,
        };

        return obj;
    }
}