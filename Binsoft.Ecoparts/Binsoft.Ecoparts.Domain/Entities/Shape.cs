using Binsoft.Ecoparts.Domain.ValueObjects;

namespace Binsoft.Ecoparts.Domain.Entities
{
    public class Shape
    {
        public ShapeId Id { get; private set; }
        public ShapeType ShapeType { get; private set; }

        private Shape(Guid id, ShapeType shapeType)
        {
            Id = new ShapeId(id);
            ShapeType = shapeType;
        }

        public static Shape Reconstitute(Guid id, ShapeType shapeType)
            => new(id, shapeType);

        public Shape(ShapeType shapeType)
        {
            if (!Enum.IsDefined(shapeType))
                throw new ArgumentOutOfRangeException(nameof(shapeType), shapeType, "Unknown shape type.");

            Id = new ShapeId(Guid.NewGuid());
            ShapeType = shapeType;
        }

        public double CalculateVolume(Dimension dimension) => ShapeType switch
        {
            ShapeType.Cylinder => Math.PI * dimension.Radius!.Value * dimension.Radius.Value * dimension.Height,
            ShapeType.Rectangular => dimension.Length!.Value * dimension.Width!.Value * dimension.Height,
            _ => throw new InvalidOperationException($"Unknown shape type: {ShapeType}")
        };
    }
}
