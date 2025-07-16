using BookingGuru.Modules.Mocks.Domain.SecondFeats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingGuru.Modules.Mocks.Infrastructure.SecondFeats;

internal sealed class SecondEntityConfiguration : IEntityTypeConfiguration<SecondEntity>
{
    public void Configure(EntityTypeBuilder<SecondEntity> builder)
    {
        builder.ToTable(nameof(SecondEntity));
    }
}