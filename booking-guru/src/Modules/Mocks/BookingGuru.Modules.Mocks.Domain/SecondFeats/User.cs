using BookingGuru.Common.Domain.Entities;

namespace BookingGuru.Modules.Mocks.Domain.SecondFeats;

public sealed class User : Entity<Guid>
{
    public User(Guid id) : base(id) { }
    public required string UserName { get; set; }
}