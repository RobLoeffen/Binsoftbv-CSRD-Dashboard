using Binsoft.CRSDdashboard.Api.Models.Entities;

namespace Binsoft.CRSDdashboard.Api.Repositories.Interfaces
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<Material>> GetAllAsync();
        Task<Material?> GetByNameAsync(string name);
    }
}
