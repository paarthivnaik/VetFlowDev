using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VetFlow.Application.Common.Interfaces;
using VetFlow.Infrastructure.Persistence;
using VetFlow.Infrastructure.Persistence.Interceptors;

namespace VetFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // ── Audit Interceptor ────────────────────────────────────────────────
        services.AddSingleton<AuditableEntityInterceptor>();

        // ── Database Provider Resolution ─────────────────────────────────────
        var providerString = configuration["DatabaseProvider"] ?? "SQLite";
        var isPostgreSql = string.Equals(providerString, "PostgreSQL", StringComparison.OrdinalIgnoreCase);

        // Platform connection string
        var platformConnectionString = (isPostgreSql
            ? configuration.GetConnectionString("PostgreSQLPlatform") ?? configuration.GetConnectionString("Platform")
            : configuration.GetConnectionString("Platform")) ?? "Data Source=../.devdata/platform.db";

        // Register PlatformDbContext
        services.AddDbContext<PlatformDbContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<AuditableEntityInterceptor>();
            options.AddInterceptors(interceptor);

            if (isPostgreSql)
            {
                options.UseNpgsql(platformConnectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(PlatformDbContext).Assembly.FullName);
                });
            }
            else
            {
                var rawPath = platformConnectionString.Replace("Data Source=", "", StringComparison.OrdinalIgnoreCase).Trim();
                var dir = Path.GetDirectoryName(rawPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                options.UseSqlite(platformConnectionString, sqliteOptions =>
                {
                    sqliteOptions.MigrationsAssembly(typeof(PlatformDbContext).Assembly.FullName);
                });
            }
        });

        // Register TenantDbContext (default to local dev template)
        var tenantConnectionString = isPostgreSql
            ? configuration.GetConnectionString("PostgreSQLTenantTemplate") ?? configuration.GetConnectionString("TenantTemplate")
            : configuration.GetConnectionString("TenantTemplate") ?? "Data Source=../.devdata/tenant-dev.db";

        services.AddDbContext<TenantDbContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<AuditableEntityInterceptor>();
            options.AddInterceptors(interceptor);

            if (isPostgreSql)
            {
                options.UseNpgsql(tenantConnectionString);
            }
            else
            {
                options.UseSqlite(tenantConnectionString);
            }
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TenantDbContext>());

        return services;
    }
}
