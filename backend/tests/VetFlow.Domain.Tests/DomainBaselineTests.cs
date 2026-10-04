using FluentAssertions;
using VetFlow.Domain.Common;
using Xunit;

namespace VetFlow.Domain.Tests;

public class DomainBaselineTests
{
    private sealed class TestDomainEvent : IDomainEvent
    {
        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
    }

    private sealed class TestAggregate : AggregateRoot<Guid>
    {
        public TestAggregate(Guid id)
        {
            Id = id;
        }

        public void DoAction()
        {
            AddDomainEvent(new TestDomainEvent());
        }
    }

    [Fact]
    public void AggregateRoot_ShouldCaptureAndClearDomainEvents()
    {
        // Arrange
        var aggregate = new TestAggregate(Guid.NewGuid());

        // Act
        aggregate.DoAction();

        // Assert
        aggregate.DomainEvents.Should().HaveCount(1);

        // Act
        aggregate.ClearDomainEvents();

        // Assert
        aggregate.DomainEvents.Should().BeEmpty();
    }
}
