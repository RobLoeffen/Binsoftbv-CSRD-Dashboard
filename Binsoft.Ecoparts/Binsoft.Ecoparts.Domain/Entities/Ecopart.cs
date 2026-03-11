using Binsoft.Ecoparts.Domain.ValueObjects;

namespace Binsoft.Ecoparts.Domain.Entities
{
    public class Ecopart
    {
        public EcopartId Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public MaterialId MaterialId { get; private set; }
        public ShapeId ShapeId { get; private set; }
        public ShapeType ShapeType { get; private set; }
        public Dimension Dimension { get; private set; }

        private Ecopart(Guid id, string name, MaterialId materialId, ShapeId shapeId, ShapeType shapeType, Dimension dimension)
        {
            Id = new EcopartId(id);
            Name = name;
            MaterialId = materialId;
            ShapeId = shapeId;
            ShapeType = shapeType;
            Dimension = dimension;
        }

        public static Ecopart Reconstitute(Guid id, string name, Guid materialId, Guid shapeId, ShapeType shapeType, Dimension dimension)
            => new(id, name, new MaterialId(materialId), new ShapeId(shapeId), shapeType, dimension);

        public Ecopart(string name, MaterialId materialId, ShapeId shapeId, ShapeType shapeType, Dimension dimension)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentNullException.ThrowIfNull(materialId);
            ArgumentNullException.ThrowIfNull(shapeId);
            ArgumentNullException.ThrowIfNull(dimension);

            Id = new EcopartId(Guid.NewGuid());
            Name = name;
            MaterialId = materialId;
            ShapeId = shapeId;
            ShapeType = shapeType;
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
