using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingGuru.Common.Infrastructure.Inbox;

public sealed class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
{
    public void Configure(EntityTypeBuilder<InboxMessage> builder)
    {
        builder.ToTable(nameof(InboxMessage));

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Content).HasMaxLength(2000);
    }
}
