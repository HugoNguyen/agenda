using BookingGuru.Common.Domain.Entities;

namespace BookingGuru.Modules.Mocks.Domain.SecondFeats;

public sealed class User : Entity<Guid>
{
    private User() { }
    public required string UserName { get; set; }
}