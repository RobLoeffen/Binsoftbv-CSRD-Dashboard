using Binsoft.Ecoparts.Application.DTOs;
using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.ValueObjects;

namespace Binsoft.Ecoparts.Application.Mappers
{
    public class EcopartMapper : IEcopartMapper
    {
        public EcopartListResponse ToListResponse(Ecopart ecopart, Material material)
        {
            return new EcopartListResponse
            {
                Id = ecopart.Id.Value,
                Name = ecopart.Name,
                Material = MapToMaterialDto(material),
                Shape = MapToShapeDto(ecopart.Shape)
            };
        }

        public EcopartDetailResponse ToDetailResponse(Ecopart ecopart, Material material)
        {
            return new EcopartDetailResponse
            {
                Id = ecopart.Id.Value,
                Name = ecopart.Name,
                Material = MapToMaterialDto(material),
                Shape = MapToShapeDto(ecopart.Shape),
                Mass = ecopart.CalculateMass(material),
                CarbonFootprint = ecopart.CalculateCarbonFootprint(material)
            };
        }

        public IEnumerable<EcopartListResponse> ToListResponse(IEnumerable<Ecopart> ecoparts, IReadOnlyDictionary<MaterialId, Material> materials)
        {
            return ecoparts.Select(e => ToListResponse(e, materials[e.MaterialId]));
        }

        private static MaterialDto MapToMaterialDto(Material material)
        {
            return new MaterialDto
            {
                Name = material.Name,
                Density = material.Density,
                EmissionFactor = material.EmissionFactor
            };
        }

        private static ShapeDto MapToShapeDto(Shape shape) => new()
        {
            ShapeType = shape.ShapeType.ToString(),
            Dimensions = shape.GetDimensions()
        };
    }
}

