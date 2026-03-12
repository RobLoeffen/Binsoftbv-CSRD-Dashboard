namespace Binsoft.Ecoparts.Application.Requests
{
    public class CreateMaterialRequest
    {
        public string Name { get; set; } = string.Empty;
        public double Density { get; set; }
        public double EmissionFactor { get; set; }
    }
}
