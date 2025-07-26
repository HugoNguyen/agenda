using BookingGuru.Modules.Mock2s.Domain.Publishes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingGuru.Modules.Mock2s.Infrastructure.Publishes;

internal sealed class PublishConfiguration : IEntityTypeConfiguration<Publish>
{
    public void Configure(EntityTypeBuilder<Publish> builder)
    {
        builder.ToTable(nameof(Publish));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .HasMaxLength(200);
    }
}