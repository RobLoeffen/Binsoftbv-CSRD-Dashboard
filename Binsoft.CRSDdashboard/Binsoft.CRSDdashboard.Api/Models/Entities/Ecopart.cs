namespace Binsoft.CRSDdashboard.Api.Models.Entities
{
    public class Ecopart
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        
        public ShapeType ShapeType { get; set; }
        
        public double Length { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? Radius { get; set; }
        
        public int MaterialId { get; set; }
        public Material Material { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        
        public double Volume => CalculateVolume();
        public double Mass => Volume * Material.Density;
        
        private double CalculateVolume()
        {
            return ShapeType switch
            {
                ShapeType.RectangularPrism => Length * Width.GetValueOrDefault() * Height.GetValueOrDefault(),
                ShapeType.TriangularPrism => 0.5 * Length * Width.GetValueOrDefault() * Height.GetValueOrDefault(),
                ShapeType.Cylinder => Math.PI * Math.Pow(Radius.GetValueOrDefault(), 2) * Height.GetValueOrDefault(),
                _ => 0
            };
        }
    }
}


