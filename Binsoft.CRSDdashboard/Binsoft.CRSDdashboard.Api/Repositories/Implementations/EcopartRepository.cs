using Binsoft.CRSDdashboard.Api.Data;
using Binsoft.CRSDdashboard.Api.Models.Entities;
using Binsoft.CRSDdashboard.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Binsoft.CRSDdashboard.Api.Repositories.Implementations
{
    public class EcopartRepository : IEcopartRepository
    {
        private readonly AppDbContext _context;

        public EcopartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ecopart>> GetAllAsync()
        {
            return await _context.Ecoparts
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Ecopart?> GetByIdAsync(int id)
        {
            return await _context.Ecoparts
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
