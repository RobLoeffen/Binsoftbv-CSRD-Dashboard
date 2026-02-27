namespace Binsoft.Ecoparts.Domain.ValueObjects;

public record Shape
{
    public ShapeType ShapeType { get; init; }
    public double Height { get; init; }
    public double? Length { get; init; }
    public double? Width { get; init; }
    public double? Radius { get; init; }

    private Shape(ShapeType shapeType, double height, double? length, double? width, double? radius)
    {
        ShapeType = shapeType;
        Height = height;
        Length = length;
        Width = width;
        Radius = radius;
    }

    public static Shape Cylinder(double radius, double height)
    {
        if (radius <= 0 || height <= 0)
            throw new ArgumentException("Dimensions must be greater than zero");

        return new Shape(ShapeType.Cylinder, height, null, null, radius);
    }

    public static Shape Rectangular(double length, double width, double height)
    {
        if (length <= 0 || width <= 0 || height <= 0)
            throw new ArgumentException("Dimensions must be greater than zero");

        return new Shape(ShapeType.Rectangular, height, length, width, null);
    }

    public double CalculateVolume() => ShapeType switch
    {
        ShapeType.Cylinder => Math.PI * Radius!.Value * Radius.Value * Height,
        ShapeType.Rectangular => Length!.Value * Width!.Value * Height,
        _ => throw new InvalidOperationException($"Unknown shape type: {ShapeType}")
    };

    public Dictionary<string, double> GetDimensions() => ShapeType switch
    {
        ShapeType.Cylinder => new() { ["Radius"] = Radius!.Value, ["Height"] = Height },
        ShapeType.Rectangular => new() { ["Length"] = Length!.Value, ["Width"] = Width!.Value, ["Height"] = Height },
        _ => throw new InvalidOperationException($"Unknown shape type: {ShapeType}")
    };
}
