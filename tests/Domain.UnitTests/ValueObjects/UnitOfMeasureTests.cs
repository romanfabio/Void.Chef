using FluentAssertions;
using NUnit.Framework;
using Void.Chef.Domain.Exceptions;
using Void.Chef.Domain.ValueObjects;

namespace Void.Chef.Domain.UnitTests.ValueObjects;

public class UnitOfMeasureTests
{
    [Test]
    public void ShouldReturnCorrectUnitOfMeasure()
    {
        var code = "kg";

        var unit = UnitOfMeasure.From(code);

        unit.Code.Should().Be(code);
    }

    [Test]
    public void ShouldThrowUnsupportedUnitOfMeasureExceptionGivenNotSupportedCode()
    {
        FluentActions.Invoking(() => UnitOfMeasure.From("ww"))
            .Should().Throw<UnsupportedUnitOfMeasureException>();
    }
}