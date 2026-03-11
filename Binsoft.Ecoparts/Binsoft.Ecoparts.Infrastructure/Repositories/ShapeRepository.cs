using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.Interfaces;
using Binsoft.Ecoparts.Domain.ValueObjects;
using Binsoft.Ecoparts.Infrastructure.Persistances;
using Microsoft.EntityFrameworkCore;

namespace Binsoft.Ecoparts.Infrastructure.Repositories;

public class ShapeRepository : IShapeRepository
{
    private readonly AppDbContext _context;

    public ShapeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Shape?> GetByIdAsync(ShapeId id)
    {
        var row = await _context.Shapes
            .FirstOrDefaultAsync(s => s.ShapeId == id.Value);

        return row is null ? null
            : Shape.Reconstitute(row.ShapeId, (ShapeType)row.ShapeType);
    }

    public async Task<IReadOnlyDictionary<ShapeId, Shape>> GetByIdsAsync(IEnumerable<ShapeId> ids)
    {
        var guids = ids.Select(id => id.Value).ToList();
        var rows = await _context.Shapes
            .Where(s => guids.Contains(s.ShapeId))
            .ToListAsync();

        return rows.ToDictionary(
            row => new ShapeId(row.ShapeId),
            row => Shape.Reconstitute(row.ShapeId, (ShapeType)row.ShapeType));
    }
}
