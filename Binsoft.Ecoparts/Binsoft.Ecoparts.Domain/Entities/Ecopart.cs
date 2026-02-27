using Binsoft.Ecoparts.Domain.ValueObjects;

namespace Binsoft.Ecoparts.Domain.Entities
{
    public class Ecopart
    {
        public EcopartId Id { get; private set; }
        public string Name { get; private set; } = string.Empty;

        public MaterialId MaterialId { get; private set; }

        public Shape Shape { get; private set; }

        private Ecopart() 
        {
            Id = new EcopartId(Guid.NewGuid());
            Name = string.Empty;
            MaterialId = null!;
            Shape = null!;
        }

        public Ecopart(string name, MaterialId materialId, Shape shape)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));
            
            Id = new EcopartId(Guid.NewGuid());
            Name = name;
            MaterialId = materialId ?? throw new ArgumentNullException(nameof(materialId));
            Shape = shape ?? throw new ArgumentNullException(nameof(shape));
        }

        public double CalculateMass(Material material)
        {
            return Shape.CalculateVolume() * material.Density;
        }

        public double CalculateCarbonFootprint(Material material)
        {
            return CalculateMass(material) * material.EmissionFactor;
        }
    }
}
