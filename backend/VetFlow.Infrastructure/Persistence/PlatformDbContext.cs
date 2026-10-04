using Microsoft.EntityFrameworkCore;
using VetFlow.Application.Common.Interfaces;
using VetFlow.Domain.Entities;
using VetFlow.Infrastructure.Persistence.Configurations;

namespace VetFlow.Infrastructure.Persistence;

public sealed class PlatformDbContext(DbContextOptions<PlatformDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<PlatformTenant> Tenants => Set<PlatformTenant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PlatformTenantConfiguration());
    }
}
