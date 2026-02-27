using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.Interfaces;
using Binsoft.Ecoparts.Domain.ValueObjects;
using Binsoft.Ecoparts.Infrastructure.Persistances;
using Microsoft.EntityFrameworkCore;

namespace Binsoft.Ecoparts.Infrastructure.Repositories;

public class MaterialRepository : IMaterialRepository
{
    private readonly AppDbContext _context;

    public MaterialRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Material?> GetByIdAsync(MaterialId id) =>
        await _context.Materials.FirstOrDefaultAsync(m => m.Id == id);

    public async Task<IReadOnlyDictionary<MaterialId, Material>> GetByIdsAsync(IEnumerable<MaterialId> ids)
    {
        var idList = ids.ToList();
        var materials = await _context.Materials
            .Where(m => idList.Contains(m.Id))
            .ToListAsync();

        return materials.ToDictionary(m => m.Id);
    }
}
