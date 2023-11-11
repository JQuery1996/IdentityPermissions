using DAL.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Api;

public static class DependencyInjection {
    public static IServiceCollection AddApi(this IServiceCollection services) {
        services.AddIdentityConfiguration();
        return services;
    }
    private static void AddIdentityConfiguration(this IServiceCollection services) {
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


        services.Configure<IdentityOptions>(options => {
            // password settings
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;

            // Lockout Settings
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;

            // User Settings
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;

        });

    }
}
