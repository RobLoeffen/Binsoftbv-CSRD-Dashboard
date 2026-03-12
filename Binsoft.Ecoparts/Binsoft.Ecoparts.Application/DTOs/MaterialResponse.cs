namespace Binsoft.Ecoparts.Application.DTOs
{
    public class MaterialResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Density { get; set; }
        public double EmissionFactor { get; set; }
    }
}
