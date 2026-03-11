namespace Binsoft.Ecoparts.Infrastructure.Entities;

public partial class Ecopart
{
    public Guid EcopartId { get; set; }

    public string EcopartName { get; set; } = null!;

    public Guid MaterialId { get; set; }

    public Guid ShapeId { get; set; }

    public double DimHeight { get; set; }

    public double? DimLength { get; set; }

    public double? DimWidth { get; set; }

    public double? DimRadius { get; set; }

    public virtual Material Material { get; set; } = null!;

    public virtual Shape Shape { get; set; } = null!;
}
