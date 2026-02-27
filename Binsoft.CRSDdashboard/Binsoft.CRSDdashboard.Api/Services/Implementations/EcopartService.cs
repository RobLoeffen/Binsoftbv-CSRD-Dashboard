using Binsoft.CRSDdashboard.Api.Mappers;
using Binsoft.CRSDdashboard.Api.Models.Responses;
using Binsoft.CRSDdashboard.Api.Repositories.Interfaces;
using Binsoft.CRSDdashboard.Api.Services.Interfaces;

namespace Binsoft.CRSDdashboard.Api.Services.Implementations
{
    public class EcopartService : IEcopartService
    {
        private readonly IEcopartRepository _ecopartRepository;
        private readonly ICo2CalculationService _co2CalculationService;
        private readonly IEcopartMapper _mapper;

        public EcopartService(
            IEcopartRepository ecopartRepository,
            ICo2CalculationService co2CalculationService,
            IEcopartMapper mapper)
        {
            _ecopartRepository = ecopartRepository;
            _co2CalculationService = co2CalculationService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EcopartResponse>> GetAllEcopartsAsync()
        {
            var ecoparts = await _ecopartRepository.GetAllAsync();
            return ecoparts.Select(_mapper.ToResponse);
        }

        public async Task<EcopartResponse?> GetEcopartByIdAsync(int id)
        {
            var ecopart = await _ecopartRepository.GetByIdAsync(id);
            return ecopart != null ? _mapper.ToResponse(ecopart) : null;
        }

        public async Task<EcopartWithEmissionsResponse?> GetEcopartWithEmissionsAsync(int id)
        {
            var ecopart = await _ecopartRepository.GetByIdAsync(id);

            if (ecopart == null)
                return null;

            var totalCo2 = await _co2CalculationService
                .CalculateCo2EquivalentAsync(ecopart.Material.Name, ecopart.Mass);

            return _mapper.ToResponseWithEmissions(ecopart, totalCo2);
        }
    }
}
