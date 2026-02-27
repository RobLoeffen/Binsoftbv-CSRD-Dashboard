namespace Binsoft.CRSDdashboard.Api.Models.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public double Co2PerKg { get; set; }
        public double Density { get; set; }
        public ICollection<Ecopart> Ecoparts { get; set; } = [];
    }
}
