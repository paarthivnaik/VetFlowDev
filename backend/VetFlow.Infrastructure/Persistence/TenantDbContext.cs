using Microsoft.EntityFrameworkCore;
using VetFlow.Application.Common.Interfaces;
using VetFlow.Domain.Entities;
using VetFlow.Infrastructure.Persistence.Configurations;

namespace VetFlow.Infrastructure.Persistence;

public class TenantDbContext(DbContextOptions<TenantDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
    }
}
