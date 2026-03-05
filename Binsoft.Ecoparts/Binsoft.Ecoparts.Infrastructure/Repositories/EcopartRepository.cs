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

        public async Task<IEnumerable<Ecopart>> GetAllAsync() =>
            await _context.Ecoparts.ToListAsync();

        public async Task<IEnumerable<Ecopart>> GetByMaterialIdAsync(MaterialId materialId, ShapeType? shapeType = null)
        {
            var query = _context.Ecoparts.Where(e => e.MaterialId == materialId);

            if (shapeType.HasValue)
            {
                var shapeIds = _context.Shapes
                    .Where(s => s.ShapeType == shapeType.Value)
                    .Select(s => s.Id);

                query = query.Where(e => shapeIds.Contains(e.ShapeId));
            }

            return await query.ToListAsync();
        }

        public async Task<Ecopart?> GetByIdAsync(EcopartId id) => 
            await _context.Ecoparts.FirstOrDefaultAsync(e => e.Id == id);
    }
}
