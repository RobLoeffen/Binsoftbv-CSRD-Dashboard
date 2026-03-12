using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.ValueObjects;

namespace Binsoft.Ecoparts.Domain.Interfaces;

public interface IMaterialRepository
{
    Task<IEnumerable<Material>> GetAllAsync();
    Task<Material?> GetByIdAsync(MaterialId id);
    Task<Material?> GetByNameAsync(string name);
    Task<Material?> AddMaterialAsync(Material material);
    Task<IReadOnlyDictionary<MaterialId, Material>> GetByIdsAsync(IEnumerable<MaterialId> ids);
}
