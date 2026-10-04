using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using VetFlow.Domain.Common;

namespace VetFlow.Infrastructure.Persistence.Interceptors;

/// <summary>
/// EF Core SaveChanges interceptor that automatically sets audit timestamps.
/// </summary>
public sealed class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateEntities(DbContext? context)
    {
        if (context is null) return;

        var utcNow = DateTime.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                var createdAtProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "CreatedAtUtc");
                if (createdAtProp is not null && (DateTime)createdAtProp.CurrentValue! == default)
                {
                    createdAtProp.CurrentValue = utcNow;
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                var updatedAtProp = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "UpdatedAtUtc");
                if (updatedAtProp is not null)
                {
                    updatedAtProp.CurrentValue = utcNow;
                }
            }
        }
    }
}
