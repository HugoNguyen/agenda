
using BookingGuru.Common.Infrastructure.Inbox;
using BookingGuru.Common.Infrastructure.Outbox;
using BookingGuru.Modules.Mock2s.Application.Abstractions.Data;
using BookingGuru.Modules.Mock2s.Domain.Publishes;
using BookingGuru.Modules.Mock2s.Infrastructure.Publishes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace BookingGuru.Modules.Mock2s.Infrastructure.Database;

public sealed class Mock2sDbContext(DbContextOptions<Mock2sDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Publish> Published { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(Schemas.Mock2s);

        // Default tables
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());

        // Module tables
        modelBuilder.ApplyConfiguration(new PublishConfiguration());
    }

    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            await Database.CurrentTransaction.DisposeAsync();
        }

        return (await Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
    }
}