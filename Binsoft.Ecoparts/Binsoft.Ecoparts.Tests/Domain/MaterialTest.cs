using Binsoft.Ecoparts.Domain.Entities;
using FluentAssertions;

namespace Binsoft.Ecoparts.Tests.Domain;

public class MaterialTest
{
    #region Construction – happy path

    [Fact]
    public void Constructor_ShouldCreateMaterial()
    {
        var name = "PET";
        var density = 2.7;
        var emissionFactor = 8.24;

        var material = new Material(name, density, emissionFactor);

        material.Id.Should().NotBeNull();
        material.Id.Value.Should().NotBeEmpty();
        material.Name.Should().Be(name);
        material.Density.Should().Be(density);
        material.EmissionFactor.Should().Be(emissionFactor);
    }

    [Fact]
    public void Constructor_ZeroEmissionFactor_ShouldSucceed()
    {
        var material = new Material("HDPE", 0.6, emissionFactor: 0);

        material.EmissionFactor.Should().Be(0);
    }

    #endregion

    #region Construction – guard clauses (Name)

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidName_ShouldThrowArgumentException(string? name)
    {
        var act = () => new Material(name!, density: 1.0, emissionFactor: 1.0);

        act.Should().Throw<ArgumentException>();
    }

    #endregion

    #region Construction – guard clauses (Density)

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-0.001)]
    public void Constructor_InvalidDensity_ShouldThrowArgumentOutOfRangeException(double density)
    {
        var act = () => new Material("LDPE", density, emissionFactor: 1.0);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    #endregion

    #region Construction – guard clauses (EmissionFactor)

    [Theory]
    [InlineData(-1)]
    [InlineData(-0.001)]
    public void Constructor_NegativeEmissionFactor_ShouldThrowArgumentOutOfRangeException(double emissionFactor)
    {
        var act = () => new Material("PET", density: 7.8, emissionFactor);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    #endregion

    #region Identity

    [Fact]
    public void Constructor_EachInstance_ShouldHaveUniqueId()
    {
        var a = new Material("PET", 2.7, 8.24);
        var b = new Material("HDPE", 7.8, 1.85);

        a.Id.Should().NotBe(b.Id);
    }

    #endregion
}
