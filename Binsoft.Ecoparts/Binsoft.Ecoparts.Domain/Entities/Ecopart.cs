using Binsoft.Ecoparts.Domain.ValueObjects;

namespace Binsoft.Ecoparts.Domain.Entities
{
    public class Ecopart
    {
        public EcopartId Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public MaterialId MaterialId { get; private set; }
        public ShapeId ShapeId { get; private set; }
        public Dimension Dimension { get; private set; }

        private Ecopart() 
        {
            Id = new EcopartId(Guid.NewGuid());
            Name = string.Empty;
            MaterialId = null!;
            ShapeId = null!;
            Dimension = null!;
        }

        public Ecopart(string name, MaterialId materialId, ShapeId shapeId, Dimension dimension)
        {       
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentNullException.ThrowIfNull(materialId);
            ArgumentNullException.ThrowIfNull(shapeId);
            ArgumentNullException.ThrowIfNull(dimension);

            Id = new EcopartId(Guid.NewGuid());
            Name = name;
            MaterialId = materialId;
            ShapeId = shapeId;
            Dimension = dimension;
        }

        public double CalculateMass(Shape shape, Material material)
        {
            return shape.CalculateVolume(Dimension) * material.Density;
        }

        public double CalculateCarbonFootprint(Shape shape, Material material)
        {
            return CalculateMass(shape, material) * material.EmissionFactor;
        }
    }
}
