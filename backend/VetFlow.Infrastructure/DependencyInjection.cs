using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VetFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Infrastructure service registrations will be added per story (VF-004 DB, VF-006 Auth, VF-043 Storage)
        return services;
    }
}
