namespace Binsoft.CRSDdashboard.Api.Models.Entities
{
    public class EmissionFactor
    {
        public int Id { get; set; }
        public required string Material { get; set; }
        public double Co2PerKg { get; set; }
    }
}
