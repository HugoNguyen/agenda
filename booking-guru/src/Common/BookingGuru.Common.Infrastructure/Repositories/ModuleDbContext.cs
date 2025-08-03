using Microsoft.EntityFrameworkCore;

namespace BookingGuru.Common.Infrastructure.Repositories;

public abstract class ModuleDbContext : DbContext
{
    private readonly IModuleDbContextBuilder _builder;

    protected ModuleDbContext(
        DbContextOptions options,
        Func<ModuleDbContextBuilder, ModuleDbContextBuilder>? configureBuilder = null)
        : base(options)
    {
        ModuleDbContextBuilder builder = new();
        _builder = configureBuilder?.Invoke(builder) ?? builder;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _builder.Configure(modelBuilder);
    }
}
