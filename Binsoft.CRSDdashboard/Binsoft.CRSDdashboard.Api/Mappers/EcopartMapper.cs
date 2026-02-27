using Binsoft.CRSDdashboard.Api.Models.Entities;
using Binsoft.CRSDdashboard.Api.Models.Responses;

namespace Binsoft.CRSDdashboard.Api.Mappers
{
    public class EcopartMapper : IEcopartMapper
    {
        public EcopartResponse ToResponse(Ecopart entity)
        {
            return new EcopartResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                ShapeType = entity.ShapeType.ToString(),
                Length = entity.Length,
                Width = entity.Width,
                Height = entity.Height,
                Radius = entity.Radius,
                Volume = entity.Volume,
                Mass = entity.Mass,
                Material = new MaterialResponse
                {
                    Id = entity.Material.Id,
                    Name = entity.Material.Name,
                    Density = entity.Material.Density,
                    Co2PerKg = entity.Material.Co2PerKg
                }
            };
        }

        public EcopartWithEmissionsResponse ToResponseWithEmissions(Ecopart entity, double co2Equivalent)
        {

            return new EcopartWithEmissionsResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                ShapeType = entity.ShapeType.ToString(),
                Length = entity.Length,
                Width = entity.Width,
                Height = entity.Height,
                Radius = entity.Radius,
                Volume = entity.Volume,
                Mass = entity.Mass,
                Material = new MaterialResponse
                {
                    Id = entity.Material.Id,
                    Name = entity.Material.Name,
                    Density = entity.Material.Density,
                    Co2PerKg = entity.Material.Co2PerKg
                },
                TotalCo2Equivalent = co2Equivalent
            };
        }
    }
}
