
using BookingGuru.Common.Infrastructure.Repositories;
using BookingGuru.Modules.Mock2s.Application.Abstractions.Data;
using BookingGuru.Modules.Mock2s.Domain.Publishes;
using BookingGuru.Modules.Mock2s.Infrastructure.Publishes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace BookingGuru.Modules.Mock2s.Infrastructure.Database;

[ApplyMigration]
public sealed class Mock2sDbContext : ModuleDbContext, IUnitOfWork
{
    internal DbSet<Publish> Published { get; set; }

    public Mock2sDbContext(DbContextOptions<Mock2sDbContext> options)
        : base(options, builder => builder.WithDefaultSchema(Schemas.Mock2s)
            .WithInbox()
            .WithOutbox())
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        base.OnModelCreating(modelBuilder);

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