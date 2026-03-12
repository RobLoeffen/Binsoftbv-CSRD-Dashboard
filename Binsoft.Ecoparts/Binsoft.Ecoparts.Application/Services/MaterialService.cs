using Binsoft.Ecoparts.Application.DTOs;
using Binsoft.Ecoparts.Application.Requests;
using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.Interfaces;
using Binsoft.Ecoparts.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Binsoft.Ecoparts.Application.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly ILogger<MaterialService> _logger;

        public MaterialService(IMaterialRepository materialRepository, ILogger<MaterialService> logger)
        {
            _materialRepository = materialRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MaterialResponse>> GetAllMaterialsAsync()
        {
            var materials = await _materialRepository.GetAllAsync();
            return materials.Select(m => new MaterialResponse
            {
                Id = m.Id.Value,
                Name = m.Name,
                Density = m.Density,
                EmissionFactor = m.EmissionFactor
            }).ToList();
        }

        public async Task<MaterialResponse?> GetMaterialByIdAsync(Guid id)
        {
            var material = await _materialRepository.GetByIdAsync(new MaterialId(id));

            if (material is null)
                return null;

            return new MaterialResponse
            {
                Id = material.Id.Value,
                Name = material.Name,
                Density = material.Density,
                EmissionFactor = material.EmissionFactor
            };
        }

        public async Task<MaterialResponse> AddMaterialAsync(CreateMaterialRequest request)
        {
            var existing = await _materialRepository.GetByNameAsync(request.Name);
            if (existing is not null)
                throw new InvalidOperationException($"A material with the name '{request.Name}' already exists.");

            Material material;
            try
            {
                material = new Material(request.Name, request.Density, request.EmissionFactor);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Invalid material data: {Message}", ex.Message);
                throw;
            }

            try
            {
                await _materialRepository.AddMaterialAsync(material);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save material '{Name}' to the database", request.Name);
                throw;
            }

            _logger.LogInformation("Material {Name} created with Id {Id}", material.Name, material.Id.Value);

            return new MaterialResponse
            {
                Id = material.Id.Value,
                Name = material.Name,
                Density = material.Density,
                EmissionFactor = material.EmissionFactor
            };
        }
    }
}
