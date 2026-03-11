using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.Interfaces;
using Binsoft.Ecoparts.Domain.ValueObjects;
using Binsoft.Ecoparts.Infrastructure.Persistances;
using Microsoft.EntityFrameworkCore;

namespace Binsoft.Ecoparts.Infrastructure.Repositories
{
    public class EcopartRepository : IEcopartRepository
    {
        private readonly AppDbContext _context;

        public EcopartRepository(AppDbContext context)
        {
            _context = context;
        }

        private static Ecopart MapToDomain(Entities.Ecopart row)
        {
            var shapeType = (ShapeType)row.Shape.ShapeType;
            var dimension = row.DimRadius.HasValue
                ? Dimension.ForCylinder(row.DimRadius.Value, row.DimHeight)
                : Dimension.ForRectangular(row.DimLength!.Value, row.DimWidth!.Value, row.DimHeight);

            return Ecopart.Reconstitute(row.EcopartId, row.EcopartName, row.MaterialId, row.ShapeId, shapeType, dimension);
        }

        public async Task<IEnumerable<Ecopart>> GetAllAsync()
        {
            var rows = await _context.Ecoparts
                .Include(e => e.Shape)
                .ToListAsync();

            return rows.Select(MapToDomain);
        }

        public async Task<IEnumerable<Ecopart>> GetByMaterialIdAsync(MaterialId materialId, ShapeType? shapeType = null)
        {
            var query = _context.Ecoparts
                .Include(e => e.Shape)
                .Where(e => e.MaterialId == materialId.Value);

            if (shapeType.HasValue)
            {
                var shapeTypeInt = (int)shapeType.Value;
                query = query.Where(e => e.Shape.ShapeType == shapeTypeInt);
            }

            var rows = await query.ToListAsync();
            return rows.Select(MapToDomain);
        }

        public async Task<Ecopart?> GetByIdAsync(EcopartId id)
        {
            var row = await _context.Ecoparts
                .Include(e => e.Shape)
                .FirstOrDefaultAsync(e => e.EcopartId == id.Value);

            return row is null ? null : MapToDomain(row);
        }
    }
}
