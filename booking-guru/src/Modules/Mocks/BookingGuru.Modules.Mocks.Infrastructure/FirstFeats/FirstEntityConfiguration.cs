using BookingGuru.Modules.Mocks.Domain.FirstFeats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingGuru.Modules.Mocks.Infrastructure.FirstFeats;

internal sealed class FirstEntityConfiguration : IEntityTypeConfiguration<FirstEntity>
{
    public void Configure(EntityTypeBuilder<FirstEntity> builder)
    {
        builder.ToTable(nameof(FirstEntity));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Field1)
            .HasMaxLength(200);
        builder.Property(x => x.Field1Nullable)
            .HasMaxLength(200);
    }
}