using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace VetFlow.Integration.Tests;

public class IntegrationBaselineTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public IntegrationBaselineTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void WebApplicationFactory_ShouldCreateClientSuccessfully()
    {
        var client = _factory.CreateClient();
        client.Should().NotBeNull();
    }
}
