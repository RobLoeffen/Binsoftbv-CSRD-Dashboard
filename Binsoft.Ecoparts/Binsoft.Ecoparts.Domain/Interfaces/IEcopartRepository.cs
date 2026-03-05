using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.ValueObjects;

namespace Binsoft.Ecoparts.Domain.Interfaces
{
    public interface IEcopartRepository
    {
        Task<IEnumerable<Ecopart>> GetAllAsync();
        Task<IEnumerable<Ecopart>> GetByMaterialIdAsync(MaterialId materialId, ShapeType? shapeType = null);
        Task<Ecopart?> GetByIdAsync(EcopartId id);
    }
}
