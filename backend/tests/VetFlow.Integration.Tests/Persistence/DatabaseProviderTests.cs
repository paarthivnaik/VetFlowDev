using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using VetFlow.Domain.Entities;
using VetFlow.Infrastructure.Persistence;
using VetFlow.Infrastructure.Persistence.Interceptors;
using Xunit;

namespace VetFlow.Integration.Tests.Persistence;

public class DatabaseProviderTests : IDisposable
{
    private readonly SqliteConnection _sqliteConnection;
    private readonly AuditableEntityInterceptor _interceptor;

    public DatabaseProviderTests()
    {
        _sqliteConnection = new SqliteConnection("Data Source=:memory:");
        _sqliteConnection.Open();
        _interceptor = new AuditableEntityInterceptor();
    }

    [Fact]
    public async Task SQLite_PlatformDbContext_ShouldCreateDatabaseAndSaveTenant()
    {
        var options = new DbContextOptionsBuilder<PlatformDbContext>()
            .UseSqlite(_sqliteConnection)
            .AddInterceptors(_interceptor)
            .Options;

        using var context = new PlatformDbContext(options);
        await context.Database.EnsureCreatedAsync();

        var tenant = new PlatformTenant(Guid.NewGuid(), "Demo Clinic", "demo-clinic");
        context.Tenants.Add(tenant);
        var savedCount = await context.SaveChangesAsync();

        savedCount.Should().Be(1);

        var retrieved = await context.Tenants.FirstOrDefaultAsync(t => t.Slug == "demo-clinic");
        retrieved.Should().NotBeNull();
        retrieved!.Name.Should().Be("Demo Clinic");
        retrieved.CreatedAtUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task SQLite_TenantDbContext_ShouldCreateDatabaseAndSaveOutboxMessage()
    {
        var options = new DbContextOptionsBuilder<TenantDbContext>()
            .UseSqlite(_sqliteConnection)
            .AddInterceptors(_interceptor)
            .Options;

        using var context = new TenantDbContext(options);
        await context.Database.EnsureCreatedAsync();

        var outboxMessage = new OutboxMessage(
            Guid.NewGuid(),
            "TenantCreatedEvent",
            "{\"TenantId\":\"123\"}",
            DateTime.UtcNow);

        context.OutboxMessages.Add(outboxMessage);
        var savedCount = await context.SaveChangesAsync();

        savedCount.Should().Be(1);

        var retrieved = await context.OutboxMessages.FirstOrDefaultAsync(o => o.Type == "TenantCreatedEvent");
        retrieved.Should().NotBeNull();
        retrieved!.ProcessedOnUtc.Should().BeNull();
    }

    public void Dispose()
    {
        _sqliteConnection.Dispose();
        GC.SuppressFinalize(this);
    }
}
