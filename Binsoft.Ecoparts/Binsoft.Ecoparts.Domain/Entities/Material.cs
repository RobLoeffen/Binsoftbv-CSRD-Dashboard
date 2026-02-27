using Binsoft.Ecoparts.Domain.ValueObjects;

namespace Binsoft.Ecoparts.Domain.Entities;

public class Material
{
    public MaterialId Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public double Density { get; private set; }
    public double EmissionFactor { get; private set; }

    private Material()
    {
        Id = new MaterialId(Guid.NewGuid());
    }

    public Material(string name, double density, double emissionFactor)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        if (density <= 0)
            throw new ArgumentException("Density must be greater than zero", nameof(density));
        if (emissionFactor < 0)
            throw new ArgumentException("Emission factor cannot be negative", nameof(emissionFactor));

        Id = new MaterialId(Guid.NewGuid());
        Name = name;
        Density = density;
        EmissionFactor = emissionFactor;
    }
}
