using BookingGuru.Modules.Mocks.Domain.PublishClones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingGuru.Modules.Mocks.Infrastructure.Publishes;

internal sealed class PublishCloneConfiguration : IEntityTypeConfiguration<PublishClone>
{
    public void Configure(EntityTypeBuilder<PublishClone> builder)
    {
        builder.ToTable(nameof(PublishClone));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .HasMaxLength(200);
    }
}