using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, ConfigurationManager configuration) {
        return services;
    }

    private static void AddPersistance(
        this IServiceCollection services,
        ConfigurationManager configuration) {
        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                );
        });
    }
}