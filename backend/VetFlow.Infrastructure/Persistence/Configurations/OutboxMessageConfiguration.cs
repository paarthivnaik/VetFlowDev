using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetFlow.Domain.Entities;

namespace VetFlow.Infrastructure.Persistence.Configurations;

public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Type)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(o => o.Content)
            .IsRequired();

        builder.Property(o => o.OccurredOnUtc)
            .IsRequired();

        builder.Property(o => o.ProcessedOnUtc);

        builder.Property(o => o.Error)
            .HasMaxLength(4000);

        builder.HasIndex(o => new { o.ProcessedOnUtc, o.OccurredOnUtc });
    }
}
