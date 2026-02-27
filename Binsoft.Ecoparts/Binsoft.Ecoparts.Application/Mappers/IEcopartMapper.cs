using Binsoft.Ecoparts.Application.DTOs;
using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.ValueObjects;

namespace Binsoft.Ecoparts.Application.Mappers
{
    public interface IEcopartMapper
    {
        EcopartListResponse ToListResponse(Ecopart ecopart, Material material);
        EcopartDetailResponse ToDetailResponse(Ecopart ecopart, Material material);
        IEnumerable<EcopartListResponse> ToListResponse(IEnumerable<Ecopart> ecoparts, IReadOnlyDictionary<MaterialId, Material> materials);
    }
}
