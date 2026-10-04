using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace VetFlow.Integration.Tests.Api;

public class ApiConventionsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiConventionsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Request_ShouldIncludeCorrelationIdHeaderInResponse()
    {
        var response = await _client.GetAsync("/api/test/success");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Headers.Contains("X-Correlation-Id").Should().BeTrue();

        var correlationId = response.Headers.GetValues("X-Correlation-Id").FirstOrDefault();
        correlationId.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Request_WithCustomCorrelationId_ShouldEchoCorrelationIdInResponse()
    {
        var customId = "custom-trace-12345";
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/test/success");
        request.Headers.Add("X-Correlation-Id", customId);

        var response = await _client.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Headers.GetValues("X-Correlation-Id").FirstOrDefault().Should().Be(customId);
    }

    [Fact]
    public async Task NotFound_ShouldReturn404ProblemDetails()
    {
        var response = await _client.GetAsync("/api/test/not-found");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        json.GetProperty("status").GetInt32().Should().Be(404);
        json.GetProperty("title").GetString().Should().Be("NotFound");
        json.GetProperty("detail").GetString().Should().Contain("42");
    }

    [Fact]
    public async Task Conflict_ShouldReturn409ProblemDetails()
    {
        var response = await _client.GetAsync("/api/test/conflict");

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        json.GetProperty("status").GetInt32().Should().Be(409);
        json.GetProperty("title").GetString().Should().Be("Conflict");
    }

    [Fact]
    public async Task ValidationException_ShouldReturn400WithErrorsInProblemDetails()
    {
        var response = await _client.GetAsync("/api/test/validation-exception");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        json.GetProperty("status").GetInt32().Should().Be(400);
        json.GetProperty("title").GetString().Should().Be("Validation Failure");
        json.GetProperty("errors").TryGetProperty("Email", out _).Should().BeTrue();
    }

    [Fact]
    public async Task UnhandledException_ShouldReturn500ProblemDetails()
    {
        var response = await _client.GetAsync("/api/test/unhandled-exception");

        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        json.GetProperty("status").GetInt32().Should().Be(500);
        json.GetProperty("title").GetString().Should().Be("Internal Server Error");
    }
}
