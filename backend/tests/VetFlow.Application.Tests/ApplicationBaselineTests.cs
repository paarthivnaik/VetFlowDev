using FluentAssertions;
using VetFlow.Shared.Results;
using Xunit;

namespace VetFlow.Application.Tests;

public class ApplicationBaselineTests
{
    [Fact]
    public void Result_Success_ShouldHoldValue()
    {
        var result = Result<string>.Success("test");

        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Value.Should().Be("test");
        result.Error.Should().BeNull();
    }

    [Fact]
    public void Result_Failure_ShouldHoldError()
    {
        var error = Error.Validation("Name", "Name is required");
        var result = Result<string>.Failure(error);

        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}
