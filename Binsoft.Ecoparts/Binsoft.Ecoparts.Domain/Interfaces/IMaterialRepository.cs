using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.ValueObjects;

namespace Binsoft.Ecoparts.Domain.Interfaces;

public interface IMaterialRepository
{
    Task<Material?> GetByIdAsync(MaterialId id);
    Task<Material?> GetByNameAsync(string name);
    Task<IReadOnlyDictionary<MaterialId, Material>> GetByIdsAsync(IEnumerable<MaterialId> ids);
}
