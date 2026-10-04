namespace VetFlow.Domain.Entities;

using VetFlow.Domain.Common;

/// <summary>
/// Platform tenant record managed in the Platform database.
/// Defines isolation and connection info for each practice.
/// </summary>
public sealed class PlatformTenant : AggregateRoot<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string? CustomConnectionString { get; private set; }
    public bool IsActive { get; private set; } = true;

    private PlatformTenant() { }

    public PlatformTenant(Guid id, string name, string slug, string? customConnectionString = null)
    {
        Id = id;
        Name = name;
        Slug = slug.ToLowerInvariant().Trim();
        CustomConnectionString = customConnectionString;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}
