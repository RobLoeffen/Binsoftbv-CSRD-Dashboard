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

    public async Task<IEnumerable<Material>> GetAllAsync()
    {
        var rows = await _context.Materials
            .ToListAsync();

        var Materials = rows.Select(row => Material.Reconstitute(row.MaterialId, row.MaterialName, row.MaterialDensity, row.MaterialEmissionFactor)).ToList();

        return Materials;
    }

    public async Task<Material?> GetByIdAsync(MaterialId id)
    {
        var row = await _context.Materials
            .FirstOrDefaultAsync(m => m.MaterialId == id.Value);

        return row is null ? null
            : Material.Reconstitute(row.MaterialId, row.MaterialName, row.MaterialDensity, row.MaterialEmissionFactor);
    }

    public async Task<Material?> GetByNameAsync(string name)
    {
        var row = await _context.Materials
            .FirstOrDefaultAsync(m => m.MaterialName.ToLower() == name.ToLower());

        return row is null ? null
            : Material.Reconstitute(row.MaterialId, row.MaterialName, row.MaterialDensity, row.MaterialEmissionFactor);
    }

    public async Task<Material?> AddMaterialAsync (Material material)
    {
        var row = new Entities.Material
        {
            MaterialId = material.Id.Value,
            MaterialName = material.Name,
            MaterialDensity = material.Density,
            MaterialEmissionFactor = material.EmissionFactor
        };

        _context.Materials.Add(row);
        await _context.SaveChangesAsync();
        return material;
    }

    public async Task<IReadOnlyDictionary<MaterialId, Material>> GetByIdsAsync(IEnumerable<MaterialId> ids)
    {
        var guids = ids.Select(id => id.Value).ToList();
        var rows = await _context.Materials
            .Where(m => guids.Contains(m.MaterialId))
            .ToListAsync();

        return rows.ToDictionary(
            row => new MaterialId(row.MaterialId),
            row => Material.Reconstitute(row.MaterialId, row.MaterialName, row.MaterialDensity, row.MaterialEmissionFactor));
    }
}
