using Binsoft.Ecoparts.Domain.ValueObjects;

namespace Binsoft.Ecoparts.Domain.Entities;

public class Material
{
    public MaterialId Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public double Density { get; private set; }
    public double EmissionFactor { get; private set; }

    private Material(Guid id, string name, double density, double emissionFactor)
    {
        Id = new MaterialId(id);
        Name = name;
        Density = density;
        EmissionFactor = emissionFactor;
    }

    public static Material Reconstitute(Guid id, string name, double density, double emissionFactor) => new(id, name, density, emissionFactor);

    public Material(string name, double density, double emissionFactor)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(density);
        ArgumentOutOfRangeException.ThrowIfNegative(emissionFactor);

        Id = new MaterialId(Guid.NewGuid());
        Name = name;
        Density = density;
        EmissionFactor = emissionFactor;
    }
}
