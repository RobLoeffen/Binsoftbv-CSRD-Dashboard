namespace Binsoft.CRSDdashboard.Api.Models.Responses
{
    public class MaterialResponse
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public double Density { get; init; }
        public double Co2PerKg { get; init; }
    }
}
