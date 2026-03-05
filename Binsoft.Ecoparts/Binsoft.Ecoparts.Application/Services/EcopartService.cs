using Binsoft.Ecoparts.Application.DTOs;
using Binsoft.Ecoparts.Application.Mappers;
using Binsoft.Ecoparts.Domain.ValueObjects;
using Binsoft.Ecoparts.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Binsoft.Ecoparts.Application.Services
{
    public class EcopartService : IEcopartService
    {
        private readonly IEcopartRepository _ecopartRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IShapeRepository _shapeRepository;
        private readonly IEcopartMapper _mapper;
        private readonly ILogger<EcopartService> _logger;

        public EcopartService(IEcopartRepository ecopartRepository, IMaterialRepository materialRepository, IShapeRepository shapeRepository, IEcopartMapper mapper, ILogger<EcopartService> logger)
        {
            _ecopartRepository = ecopartRepository;
            _materialRepository = materialRepository;
            _shapeRepository = shapeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<EcopartListResponse>> GetAllEcopartsAsync()
        {
            _logger.LogInformation("Fetching all ecoparts");

            var ecoparts = await _ecopartRepository.GetAllAsync();
            var ecopartList = ecoparts.ToList();

            _logger.LogInformation("Fetched {Count} ecoparts", ecopartList.Count);

            var materialIds = ecopartList.Select(e => e.MaterialId).Distinct();
            var materials = await _materialRepository.GetByIdsAsync(materialIds);

            var shapeIds = ecopartList.Select(e => e.ShapeId).Distinct();
            var shapes = await _shapeRepository.GetByIdsAsync(shapeIds);

            return _mapper.ToListResponse(ecopartList, materials, shapes);
        }

        public async Task<IEnumerable<EcopartListResponse>?> GetEcopartsByMaterialAsync(string materialName, string? shapeType = null)
        {
            ShapeType? parsedShapeType = null;

            if (!string.IsNullOrWhiteSpace(shapeType))
            {
                if (!Enum.TryParse<ShapeType>(shapeType, ignoreCase: true, out var result))
                    throw new ArgumentException($"Invalid shape type '{shapeType}'. Valid values: {string.Join(", ", Enum.GetNames<ShapeType>())}.");

                parsedShapeType = result;
            }

            _logger.LogInformation("Fetching ecoparts for material: {MaterialName}, shape filter: {ShapeType}", materialName, shapeType ?? "none");

            var material = await _materialRepository.GetByNameAsync(materialName);

            if (material == null)
            {
                _logger.LogWarning("Material '{MaterialName}' was not found", materialName);
                return null;
            }

            var ecoparts = (await _ecopartRepository.GetByMaterialIdAsync(material.Id, parsedShapeType)).ToList();

            _logger.LogInformation("Fetched {Count} ecoparts for material '{MaterialName}'", ecoparts.Count, materialName);

            var materialDict = new Dictionary<MaterialId, Domain.Entities.Material> { { material.Id, material } };

            var shapeIds = ecoparts.Select(e => e.ShapeId).Distinct();
            var shapes = await _shapeRepository.GetByIdsAsync(shapeIds);

            return _mapper.ToListResponse(ecoparts, materialDict, shapes);
        }

        public async Task<EcopartDetailResponse?> GetEcopartByIdAsync(Guid id)
        {
            _logger.LogInformation("Fetching ecopart with ID {EcopartId}", id);

            var ecopart = await _ecopartRepository.GetByIdAsync(new EcopartId(id));

            if (ecopart == null)
            {
                _logger.LogWarning("Ecopart with ID {EcopartId} was not found", id);
                return null;
            }

            var material = await _materialRepository.GetByIdAsync(ecopart.MaterialId);

            if (material == null)
            {
                _logger.LogWarning("Material with ID {MaterialId} for ecopart {EcopartId} was not found", ecopart.MaterialId, id);
                return null;
            }

            var shape = await _shapeRepository.GetByIdAsync(ecopart.ShapeId);

            if (shape == null)
            {
                _logger.LogWarning("Shape with ID {ShapeId} for ecopart {EcopartId} was not found", ecopart.ShapeId, id);
                return null;
            }

            return _mapper.ToDetailResponse(ecopart, shape, material);
        }
    }
}
