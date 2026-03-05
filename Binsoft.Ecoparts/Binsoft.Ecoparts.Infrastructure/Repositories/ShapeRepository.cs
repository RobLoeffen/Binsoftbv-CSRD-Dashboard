using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.Interfaces;
using Binsoft.Ecoparts.Domain.ValueObjects;
using Binsoft.Ecoparts.Infrastructure.Persistances;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Binsoft.Ecoparts.Infrastructure.Repositories;

public class ShapeRepository : IShapeRepository
{
    private readonly AppDbContext _context;

    public ShapeRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Shape?> GetByIdAsync(ShapeId id) =>
        await _context.Shapes.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<IReadOnlyDictionary<ShapeId, Shape>> GetByIdsAsync(IEnumerable<ShapeId> ids)
    {
        var idList = ids.ToList();
        var shapes = await _context.Shapes
            .Where(s => idList.Contains(s.Id))
            .ToListAsync();

        return shapes.ToDictionary(m => m.Id);
    }
}
