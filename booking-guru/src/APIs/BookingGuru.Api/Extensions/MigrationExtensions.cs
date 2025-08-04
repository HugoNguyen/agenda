using BookingGuru.Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookingGuru.Api.Extensions;

internal static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        var serviceProvider = scope.ServiceProvider;

        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(SafeGetTypes)
            .Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                typeof(DbContext).IsAssignableFrom(t) &&
                t.IsPublic &&
                t.GetCustomAttribute<ApplyMigrationAttribute>() != null)
            .ToList();

        foreach (var dbContextType in dbContextTypes)
        {
            if (serviceProvider.GetService(dbContextType) is DbContext context)
            {
                context.Database.Migrate();
            }
        }
    }

    // Prevent ReflectionTypeLoadException from breaking the scan
    private static IEnumerable<Type> SafeGetTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            return ex.Types.Where(t => t is not null)!;
        }
    }
}
