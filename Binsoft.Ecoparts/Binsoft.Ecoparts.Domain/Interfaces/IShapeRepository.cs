using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.ValueObjects;

namespace Binsoft.Ecoparts.Domain.Interfaces;

public interface IShapeRepository
{
    Task<Shape?> GetByIdAsync(ShapeId id);
    Task<IReadOnlyDictionary<ShapeId, Shape>> GetByIdsAsync(IEnumerable<ShapeId> ids);
}

