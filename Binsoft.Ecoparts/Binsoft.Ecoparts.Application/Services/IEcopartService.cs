using Binsoft.Ecoparts.Application.DTOs;

namespace Binsoft.Ecoparts.Application.Services
{
    public interface IEcopartService
    {
        Task<IEnumerable<EcopartListResponse>> GetAllEcopartsAsync();
        Task<EcopartDetailResponse?> GetEcopartByIdAsync(Guid id);
    }
}
