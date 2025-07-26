using BookingGuru.Modules.Mock2s.Infrastructure.Database;
using BookingGuru.Modules.Mocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BookingGuru.Api.Extensions;

internal static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<MocksDbContext>(scope);
        ApplyMigration<Mock2sDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
