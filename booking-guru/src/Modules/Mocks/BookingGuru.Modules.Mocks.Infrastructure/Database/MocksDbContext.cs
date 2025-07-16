
using BookingGuru.Common.Infrastructure.Data;
using BookingGuru.Common.Infrastructure.Inbox;
using BookingGuru.Common.Infrastructure.Outbox;
using BookingGuru.Modules.Mocks.Application.Abstractions.Data;
using BookingGuru.Modules.Mocks.Domain.FirstFeats;
using BookingGuru.Modules.Mocks.Domain.PublishClones;
using BookingGuru.Modules.Mocks.Domain.SecondFeats;
using BookingGuru.Modules.Mocks.Infrastructure.FirstFeats;
using BookingGuru.Modules.Mocks.Infrastructure.Publishes;
using BookingGuru.Modules.Mocks.Infrastructure.SecondFeats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace BookingGuru.Modules.Mocks.Infrastructure.Database;

public sealed class MocksDbContext(DbContextOptions<MocksDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<FirstEntity> FirstEntities { get; set; }
    internal DbSet<SecondEntity> SecondEntities { get; set; }
    internal DbSet<User> Users { get; set; }
    internal DbSet<PublishClone> PublishClones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(Schemas.Mocks);

        // Default tables
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());

        // Module tables
        modelBuilder.ApplyConfiguration(new FirstEntityConfiguration());

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new SecondEntityConfiguration());

        modelBuilder.ApplyConfiguration(new PublishCloneConfiguration());
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