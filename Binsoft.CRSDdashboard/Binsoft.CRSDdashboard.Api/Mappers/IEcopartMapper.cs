using Binsoft.CRSDdashboard.Api.Models.Entities;
using Binsoft.CRSDdashboard.Api.Models.Responses;

namespace Binsoft.CRSDdashboard.Api.Mappers
{
    public interface IEcopartMapper
    {
        EcopartResponse ToResponse(Ecopart entity);
        EcopartWithEmissionsResponse ToResponseWithEmissions(Ecopart entity, double co2Equivalent);
    }
}
