using BookingGuru.Modules.Mocks.Domain.SecondFeats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingGuru.Modules.Mocks.Infrastructure.SecondFeats;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
    }
}