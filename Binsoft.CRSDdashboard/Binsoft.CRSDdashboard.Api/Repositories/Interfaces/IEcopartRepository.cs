using Binsoft.CRSDdashboard.Api.Models.Entities;

namespace Binsoft.CRSDdashboard.Api.Repositories.Interfaces
{
    public interface IEcopartRepository
    {
        Task<IEnumerable<Ecopart>> GetAllAsync();
        Task<Ecopart?> GetByIdAsync(int id);
    }
}
