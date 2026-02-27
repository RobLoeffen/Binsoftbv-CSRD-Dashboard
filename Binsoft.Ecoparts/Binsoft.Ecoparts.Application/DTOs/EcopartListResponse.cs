namespace Binsoft.Ecoparts.Application.DTOs
{
    public class EcopartListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public MaterialDto Material { get; set; } = null!;
        public ShapeDto Shape { get; set; } = null!;
    }

    public class MaterialDto
    {
        public string Name { get; set; } = string.Empty;
        public double Density { get; set; }
        public double EmissionFactor { get; set; }
    }

    public class ShapeDto
    {
        public string ShapeType { get; set; } = string.Empty;
        public Dictionary<string, double> Dimensions { get; set; } = [];
    }
}
