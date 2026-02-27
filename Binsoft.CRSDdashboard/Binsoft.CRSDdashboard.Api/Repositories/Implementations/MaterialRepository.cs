using Binsoft.CRSDdashboard.Api.Data;
using Binsoft.CRSDdashboard.Api.Models.Entities;
using Binsoft.CRSDdashboard.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Binsoft.CRSDdashboard.Api.Repositories.Implementations
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly AppDbContext _context;

        public MaterialRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Material>> GetAllAsync()
        {
            return await _context.Material
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Material?> GetByNameAsync(string name)
        {
            return await _context.Material
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Name == name);
        }
    }
}
