namespace Binsoft.CRSDdashboard.Api.Models.Responses
{
    public class EcopartWithEmissionsResponse
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required string ShapeType { get; init; }
        public double Length { get; init; }
        public double? Width { get; init; }
        public double? Height { get; init; }
        public double? Radius { get; init; }
        public double Volume { get; init; }
        public double Mass { get; init; }
        public required MaterialResponse Material { get; init; }
        public double TotalCo2Equivalent { get; init; }
    }
}
