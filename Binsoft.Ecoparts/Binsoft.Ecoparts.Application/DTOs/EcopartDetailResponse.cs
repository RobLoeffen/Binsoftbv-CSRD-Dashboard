namespace Binsoft.Ecoparts.Application.DTOs
{
    public class EcopartDetailResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public MaterialDto Material { get; set; } = null!;
        public ShapeDto Shape { get; set; } = null!;
        public double Mass { get; set; }
        public double CarbonFootprint { get; set; }
    }
}
