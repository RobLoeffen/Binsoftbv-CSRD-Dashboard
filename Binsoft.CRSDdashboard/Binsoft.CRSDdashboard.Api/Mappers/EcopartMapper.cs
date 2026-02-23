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
                Length = entity.Length,
                Width = entity.Width,
                Weight = entity.Weight,
                Material = entity.Material
            };
        }

        public EcopartWithEmissionsResponse ToResponseWithEmissions(Ecopart entity, double co2Equivalent, double co2PerKg)
        {
            return new EcopartWithEmissionsResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Length = entity.Length,
                Width = entity.Width,
                Weight = entity.Weight,
                Material = entity.Material,
                TotalCo2Equivalent = co2Equivalent,
                Co2PerKg = co2PerKg
            };
        }
    }
}
