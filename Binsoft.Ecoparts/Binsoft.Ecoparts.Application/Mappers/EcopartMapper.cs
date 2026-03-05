using Binsoft.Ecoparts.Application.DTOs;
using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.ValueObjects;

namespace Binsoft.Ecoparts.Application.Mappers
{
    public class EcopartMapper : IEcopartMapper
    {
        public EcopartListResponse ToListResponse(Ecopart ecopart, Shape shape, Material material)
        {
            return new EcopartListResponse
            {
                Id = ecopart.Id.Value,
                Name = ecopart.Name,
                Material = MapToMaterialDto(material),
                Shape = MapToShapeDto(shape, ecopart.Dimension)
            };
        }

        public EcopartDetailResponse ToDetailResponse(Ecopart ecopart, Shape shape, Material material)
        {
            return new EcopartDetailResponse
            {
                Id = ecopart.Id.Value,
                Name = ecopart.Name,
                Material = MapToMaterialDto(material),
                Shape = MapToShapeDto(shape, ecopart.Dimension),
                Mass = ecopart.CalculateMass(shape, material),
                CarbonFootprint = ecopart.CalculateCarbonFootprint(shape, material)
            };
        }

        public IEnumerable<EcopartListResponse> ToListResponse(IEnumerable<Ecopart> ecoparts, IReadOnlyDictionary<MaterialId, Material> materials, IReadOnlyDictionary<ShapeId, Shape> shapes)
        {
            return ecoparts.Select(e => ToListResponse(e, shapes[e.ShapeId], materials[e.MaterialId]));
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

        private static ShapeDto MapToShapeDto(Shape shape, Dimension dimension) => new()
        {
            ShapeType = shape.ShapeType.ToString(),
            Dimensions = dimension.GetDimensions()
        };
    }
}

