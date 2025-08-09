using BookingGuru.Common.Application.Repositories;
using BookingGuru.Common.Domain.Attributes;
using BookingGuru.Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookingGuru.Common.Infrastructure.Repositories;

/// <summary>
/// Auto register all reposocittory
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddModuleDbContext<TDbContext>(
        this IServiceCollection services,
        Action<IServiceProvider, DbContextOptionsBuilder> options)
        where TDbContext : DbContext
    {
        // Register the DbContext
        services.AddDbContext<TDbContext>(options);

        // Automatically add GenericRepositories
        services.AddGenericRepositories<TDbContext>();

        // Automatically add decorated repositoories
        services.AddDecoratedRepositories<TDbContext>();

        return services;
    }

    private static IServiceCollection AddGenericRepositories<TDbContext>(this IServiceCollection services)
         where TDbContext : DbContext
    {
        using var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<TDbContext>();

        var model = dbContext.Model.GetEntityTypes();

        foreach (var entityType in model)
        {
            var clrType = entityType.ClrType;

            // Skip types that do not implement IEntity<T>
            var ientityInterface = clrType.GetInterfaces()
                .FirstOrDefault(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IEntity<>));

            if (ientityInterface == null)
                continue; // skip non-IEntity types

            var primaryKeyType = ientityInterface.GetGenericArguments()[0];

            var genericRepoInterface = typeof(IRepository<,>).MakeGenericType(clrType, primaryKeyType);
            var simpleRepoInterface = typeof(IRepository<>).MakeGenericType(clrType);
            var implementationType = typeof(RepositoryBase<,,>).MakeGenericType(typeof(TDbContext), clrType, primaryKeyType);

            services.AddTransient(genericRepoInterface, implementationType);
            services.AddTransient(simpleRepoInterface, implementationType);
        }

        return services;
    }

    private static void AddDecoratedRepositories<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
    {
        services.Scan(scan => scan
            .FromAssemblyOf<TDbContext>()
            .AddClasses(c => c.WithAttribute<ModularRepositoryAttribute>().Where(t => !t.IsAbstract))
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );
    }
}