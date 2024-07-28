using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PromoCodeFactory.EntityFramework;

public static class EntityFrameworkConfigurator
{
    public static IServiceCollection AddEntityFramework
        (this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DatabaseContext>(optionsBuilder => optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlite(connectionString));
        
        return services;
    }
}