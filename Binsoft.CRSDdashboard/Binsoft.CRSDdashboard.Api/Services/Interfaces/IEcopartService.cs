using Binsoft.CRSDdashboard.Api.Models.Responses;

namespace Binsoft.CRSDdashboard.Api.Services.Interfaces
{
    public interface IEcopartService
    {
        Task<IEnumerable<EcopartResponse>> GetAllEcopartsAsync();
        Task<EcopartResponse?> GetEcopartByIdAsync(int id);
        Task<EcopartWithEmissionsResponse?> GetEcopartWithEmissionsAsync(int id);
    }
}
