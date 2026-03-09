using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.ValueObjects;
using FluentAssertions;

namespace Binsoft.Ecoparts.Tests.Domain;

public class EcopartTest
{
    #region Construction – happy path
    [Fact]
    public void Constructor_ShouldCreateEcopart()
    {
        var name = "Ecopart";
        var materialId = new MaterialId(Guid.NewGuid());
        var shapeId = new ShapeId(Guid.NewGuid());
        var dimension = Dimension.ForRectangular(10, 20, 30);

        var act = new Ecopart(name, materialId, shapeId, dimension);

        act.Id.Should().NotBeNull();
        act.Id.Value.Should().NotBeEmpty();
        act.Name.Should().Be(name);
        act.MaterialId.Should().Be(materialId);
        act.ShapeId.Should().Be(shapeId);
        act.Dimension.Should().Be(dimension);
    }
    #endregion

    #region Construction – guard clauses (Name)
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidName_ShouldThrowArgumentException(string? name)
    {
        var materialId = new MaterialId(Guid.NewGuid());
        var shapeId = new ShapeId(Guid.NewGuid());
        var dimension = Dimension.ForRectangular(10, 20, 30);

        var act = () => new Ecopart(name!, materialId, shapeId, dimension);

        act.Should().Throw<ArgumentException>();
    }
    #endregion

    #region Construction – guard clauses (MaterialId)
    [Fact]
    public void Constructor_InvalidMaterialId_ShouldThrowArgumentNullException()
    {
        var name = "Ecopart";
        var shapeId = new ShapeId(Guid.NewGuid());
        var dimension = Dimension.ForRectangular(10, 20, 30);

        var act = () => new Ecopart(name, materialId: null!, shapeId, dimension);

        act.Should().Throw<ArgumentNullException>();
    }
    #endregion

    #region Construction – guard clauses (ShapeId)
    [Fact]
    public void Constructor_InvalidShapeId_ShouldThrowArgumentNullException()
    {
        var name = "Ecopart";
        var materialId = new MaterialId(Guid.NewGuid());
        var dimension = Dimension.ForRectangular(10, 20, 30);

        var act = () => new Ecopart(name, materialId, null!, dimension);

       act.Should().Throw<ArgumentNullException>();
    }
    #endregion

    #region Construction – guard clauses (Dimension)
    [Fact]
    public void Constructor_InvalidDimension_ShouldThrowArgumentNullException()
    {
        var name = "Ecopart";
        var materialId = new MaterialId(Guid.NewGuid());
        var shapeId = new ShapeId(Guid.NewGuid());

        var act = () => new Ecopart(name, materialId, shapeId, null!);

        act.Should().Throw<ArgumentNullException>();
    }
    #endregion

    #region Calculations - happy path
    [Fact]
    public void CalculateMass_Rectangular_ShouldReturnCorrectMass()
    {
        var name = "Ecopart";
        var materialId = new MaterialId(Guid.NewGuid());
        var shapeId = new ShapeId(Guid.NewGuid());
        var dimension = Dimension.ForRectangular(10, 20, 30);
        var ecopart = new Ecopart(name, materialId, shapeId, dimension);
        var shape = new Shape(ShapeType.Rectangular);
        var material = new Material("PET", density: 2.0, emissionFactor: 1.0);

        var act = ecopart.CalculateMass(shape, material);

        act.Should().Be(12000);
    }

    [Fact]
    public void CalculateMass_Cylinder_ShouldReturnCorrectMass()
    {
        var name = "Ecopart";
        var materialId = new MaterialId(Guid.NewGuid());
        var shapeId = new ShapeId(Guid.NewGuid());
        var dimension = Dimension.ForCylinder(radius: 5, height: 10);
        var ecopart = new Ecopart(name, materialId, shapeId, dimension);
        var shape = new Shape(ShapeType.Cylinder);
        var material = new Material("PET", density: 2.0, emissionFactor: 1.0);

        var act = ecopart.CalculateMass(shape, material);

        act.Should().Be((Math.PI * 5 * 5 * 10) * 2);
    }

    [Fact]
    public void CalculateCarbonFootprint_Rectangular_ShouldReturnCorrectCarbonFootprint()
    {
        var name = "Ecopart";
        var materialId = new MaterialId(Guid.NewGuid());
        var shapeId = new ShapeId(Guid.NewGuid());
        var dimension = Dimension.ForRectangular(10, 20, 30);
        var ecopart = new Ecopart(name, materialId, shapeId, dimension);
        var shape = new Shape(ShapeType.Rectangular);
        var material = new Material("PET", density: 2.0, emissionFactor: 1.5);

        var act = ecopart.CalculateCarbonFootprint(shape, material);

        act.Should().Be(18000);
    }

    [Fact]
    public void CalculateCarbonFootprint_Cylinder_ShouldReturnCorrectCarbonFootprint()
    {
        var name = "Ecopart";
        var materialId = new MaterialId(Guid.NewGuid());
        var shapeId = new ShapeId(Guid.NewGuid());
        var dimension = Dimension.ForCylinder(radius: 5, height: 10);
        var ecopart = new Ecopart(name, materialId, shapeId, dimension);
        var shape = new Shape(ShapeType.Cylinder);
        var material = new Material("PET", density: 2.0, emissionFactor: 1.5);
        var mass = (Math.PI * 5 * 5 * 10) * 2;
        var expected = mass * 1.5;

       var act = ecopart.CalculateCarbonFootprint(shape, material);

        act.Should().Be(expected);
    }
    #endregion

    #region Identity

    [Fact]
    public void Constructor_EachInstance_ShouldHaveUniqueId()
    {
        var name = "Ecopart";
        var materialId = new MaterialId(Guid.NewGuid());
        var shapeId = new ShapeId(Guid.NewGuid());
        var dimension = Dimension.ForRectangular(10, 20, 30);

        var a = new Ecopart(name, materialId, shapeId, dimension);
        var b = new Ecopart(name, materialId, shapeId, dimension);

        a.Id.Should().NotBe(b.Id);
    }

    #endregion
}
