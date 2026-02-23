using Binsoft.CRSDdashboard.Api.Models.Entities;

namespace Binsoft.CRSDdashboard.Api.Repositories.Interfaces
{
    public interface IEmissionFactorRepository
    {
        Task<IEnumerable<EmissionFactor>> GetAllAsync();
        Task<EmissionFactor?> GetByMaterialAsync(string material);
    }
}
