using Binsoft.Ecoparts.Application.DTOs;
using Binsoft.Ecoparts.Application.Mappers;
using Binsoft.Ecoparts.Domain.ValueObjects;
using Binsoft.Ecoparts.Domain.Interfaces;

namespace Binsoft.Ecoparts.Application.Services
{
    public class EcopartService : IEcopartService
    {
        private readonly IEcopartRepository _ecopartRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IEcopartMapper _mapper;

        public EcopartService(IEcopartRepository ecopartRepository, IMaterialRepository materialRepository, IEcopartMapper mapper)
        {
            _ecopartRepository = ecopartRepository;
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EcopartListResponse>> GetAllEcopartsAsync()
        {
            var ecoparts = await _ecopartRepository.GetAllAsync();
            var ecopartList = ecoparts.ToList();

            var materialIds = ecopartList.Select(e => e.MaterialId).Distinct();
            var materials = await _materialRepository.GetByIdsAsync(materialIds);

            return _mapper.ToListResponse(ecopartList, materials);
        }

        public async Task<EcopartDetailResponse?> GetEcopartByIdAsync(Guid id)
        {
            var ecopart = await _ecopartRepository.GetByIdAsync(new EcopartId(id));
            
            if (ecopart == null)
                return null;

            var material = await _materialRepository.GetByIdAsync(ecopart.MaterialId);

            if (material == null)
                return null;

            return _mapper.ToDetailResponse(ecopart, material);
        }
    }
}
