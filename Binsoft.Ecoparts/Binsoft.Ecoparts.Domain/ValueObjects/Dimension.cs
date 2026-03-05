namespace Binsoft.Ecoparts.Domain.ValueObjects;

public record Dimension
{
    public double Height { get; init; }
    public double? Length { get; init; }
    public double? Width { get; init; }
    public double? Radius { get; init; }

    private Dimension(double height, double? length, double? width, double? radius)
    {
        Height = height;
        Length = length;
        Width = width;
        Radius = radius;
    }

    public static Dimension ForCylinder(double radius, double height)
    {
        if (radius <= 0 || height <= 0)
            throw new ArgumentException("Dimensions must be greater than zero");

        return new Dimension(height, null, null, radius);
    }

    public static Dimension ForRectangular(double length, double width, double height)
    {
        if (length <= 0 || width <= 0 || height <= 0)
            throw new ArgumentException("Dimensions must be greater than zero");

        return new Dimension(height, length, width, null);
    }

    public Dictionary<string, double> GetDimensions()
    {
        var dimensions = new Dictionary<string, double> { ["Height"] = Height };

        if (Radius.HasValue)
            dimensions["Radius"] = Radius.Value;
        if (Length.HasValue)
            dimensions["Length"] = Length.Value;
        if (Width.HasValue)
            dimensions["Width"] = Width.Value;

        return dimensions;
    }
}


