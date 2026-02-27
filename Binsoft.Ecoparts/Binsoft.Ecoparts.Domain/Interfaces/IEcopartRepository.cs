using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.ValueObjects;

namespace Binsoft.Ecoparts.Domain.Interfaces
{
    public interface IEcopartRepository
    {
        Task<IEnumerable<Ecopart>> GetAllAsync();
        Task<Ecopart?> GetByIdAsync(EcopartId id);
    }
}
