using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetFlow.Domain.Entities;

namespace VetFlow.Infrastructure.Persistence.Configurations;

public sealed class PlatformTenantConfiguration : IEntityTypeConfiguration<PlatformTenant>
{
    public void Configure(EntityTypeBuilder<PlatformTenant> builder)
    {
        builder.ToTable("platform_tenants");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Slug)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(t => t.Slug)
            .IsUnique();

        builder.Property(t => t.CustomConnectionString)
            .HasMaxLength(1000);

        builder.Property(t => t.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(t => t.CreatedAtUtc)
            .IsRequired();

        builder.Property(t => t.UpdatedAtUtc);
    }
}
