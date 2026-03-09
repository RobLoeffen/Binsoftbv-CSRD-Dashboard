using Binsoft.Ecoparts.Domain.ValueObjects;
using Binsoft.Ecoparts.Domain.Entities;
using FluentAssertions;


namespace Binsoft.Ecoparts.Tests.Domain;

public class ShapeTest
{
    #region Construction – happy path

    [Fact]
    public void Constructor_ShouldCreateShape()
    {
        var shapeType = ShapeType.Cylinder;

        var shape = new Shape(shapeType);

        shape.Id.Should().NotBeNull();
        shape.Id.Value.Should().NotBeEmpty();
        shape.ShapeType.Should().Be(shapeType);
    }

    #endregion

    #region CalculateVolume – happy path

    [Fact]
    public void CalculateVolume_Cylinder_ShouldReturnCalculatedValue()
    {
        var shape = new Shape(ShapeType.Cylinder);
        var dimensions = Dimension.ForCylinder(radius: 5.0, height: 10.0);
        var expected = Math.PI * 5.0 * 5.0 * 10.0;

        var volume = shape.CalculateVolume(dimensions);

        volume.Should().Be(expected);
    }

    [Fact]
    public void CalculateVolume_Rectangular_ShouldReturnCalculatedValue()
    {
        var shape = new Shape(ShapeType.Rectangular);
        var dimensions = Dimension.ForRectangular(length: 5.0, width: 50.0, height: 10.0);
        var expected = 5.0 * 50.0 * 10.0;

        var volume = shape.CalculateVolume(dimensions);

        volume.Should().Be(expected);
    }

    #endregion

    #region Construction – guard clauses (ShapeType)

    [Theory]
    [InlineData(-1)]
    [InlineData(999)]
    public void Constructor_InvalidShapeType_ShouldThrowArgumentOutOfRangeException(int invalidValue)
    {
        var act = () => new Shape((ShapeType)invalidValue);

        act.Should().Throw<ArgumentOutOfRangeException>();

    }

    #endregion


    #region Identity

    [Fact]
    public void Id_ShouldBeUnique()
    {
        var shape1 = new Shape(ShapeType.Cylinder);
        var shape2 = new Shape(ShapeType.Cylinder);

        shape1.Id.Should().NotBe(shape2.Id);
    }

    #endregion

}

