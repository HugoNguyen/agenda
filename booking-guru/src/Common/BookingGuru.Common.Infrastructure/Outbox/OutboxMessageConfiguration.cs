﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingGuru.Common.Infrastructure.Outbox;

public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(nameof(OutboxMessage));

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Content).HasMaxLength(2000);
    }
}
