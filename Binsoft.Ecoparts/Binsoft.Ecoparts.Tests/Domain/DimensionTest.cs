using Binsoft.Ecoparts.Domain.ValueObjects;
using FluentAssertions;

namespace Binsoft.Ecoparts.Tests.Domain;

public class DimensionTest
{
    #region ForCylinder – happy path

    [Fact]
    public void ForCylinder_ShouldCreateDimension()
    {
        var dimension = Dimension.ForCylinder(radius: 5.0, height: 10.0);

        dimension.Radius.Should().Be(5.0);
        dimension.Height.Should().Be(10.0);
        dimension.Length.Should().BeNull();
        dimension.Width.Should().BeNull();
    }

    #endregion

    #region ForRectangular – happy path

    [Fact]
    public void ForRectangular_ShouldCreateDimension()
    {
        var dimension = Dimension.ForRectangular(length: 5.0, width: 50.0, height: 10.0);

        dimension.Length.Should().Be(5.0);
        dimension.Width.Should().Be(50.0);
        dimension.Height.Should().Be(10.0);
        dimension.Radius.Should().BeNull();
    }

    #endregion

    #region ForCylinder – guard clauses

    [Theory]
    [InlineData(0, 10.0)]
    [InlineData(-1, 10.0)]
    [InlineData(5.0, 0)]
    [InlineData(5.0, -1)]
    public void ForCylinder_InvalidDimensions_ShouldThrowArgumentException(double radius, double height)
    {
        var act = () => Dimension.ForCylinder(radius, height);

        act.Should().Throw<ArgumentException>();
    }

    #endregion

    #region ForRectangular – guard clauses

    [Theory]
    [InlineData(0, 50.0, 10.0)]
    [InlineData(-1, 50.0, 10.0)]
    [InlineData(5.0, 0, 10.0)]
    [InlineData(5.0, -1, 10.0)]
    [InlineData(5.0, 50.0, 0)]
    [InlineData(5.0, 50.0, -1)]
    public void ForRectangular_InvalidDimensions_ShouldThrowArgumentException(double length, double width, double height)
    {
        var act = () => Dimension.ForRectangular(length, width, height);

        act.Should().Throw<ArgumentException>();
    }

    #endregion
}
