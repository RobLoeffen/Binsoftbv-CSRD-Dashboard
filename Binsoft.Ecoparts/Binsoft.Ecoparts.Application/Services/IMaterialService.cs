using Binsoft.Ecoparts.Application.DTOs;
using Binsoft.Ecoparts.Application.Requests;

namespace Binsoft.Ecoparts.Application.Services
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialResponse>> GetAllMaterialsAsync();
        Task<MaterialResponse?> GetMaterialByIdAsync(Guid id);
        Task<MaterialResponse> AddMaterialAsync(CreateMaterialRequest request);
    }
}
