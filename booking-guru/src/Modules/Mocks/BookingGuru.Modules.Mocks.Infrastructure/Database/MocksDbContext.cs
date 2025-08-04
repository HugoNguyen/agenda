
using BookingGuru.Common.Infrastructure.Repositories;
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

[ApplyMigration]
public sealed class MocksDbContext : ModuleDbContext, IUnitOfWork
{
    internal DbSet<FirstEntity> FirstEntities { get; set; }
    internal DbSet<SecondEntity> SecondEntities { get; set; }
    internal DbSet<User> Users { get; set; }
    internal DbSet<PublishClone> PublishClones { get; set; }

    public MocksDbContext(DbContextOptions<MocksDbContext> options)
        : base(options, builder => builder.WithDefaultSchema(Schemas.Mocks)
            .WithInbox()
            .WithOutbox())
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        base.OnModelCreating(modelBuilder);

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