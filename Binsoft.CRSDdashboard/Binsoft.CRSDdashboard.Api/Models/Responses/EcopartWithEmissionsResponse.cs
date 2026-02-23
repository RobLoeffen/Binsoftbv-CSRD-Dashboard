namespace Binsoft.CRSDdashboard.Api.Models.Responses
{
    public class EcopartWithEmissionsResponse
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public double Length { get; init; }
        public double Width { get; init; }
        public double Weight { get; init; }
        public required string Material { get; init; }
        public double TotalCo2Equivalent { get; init; }
        public double Co2PerKg { get; init; }
    }
}
