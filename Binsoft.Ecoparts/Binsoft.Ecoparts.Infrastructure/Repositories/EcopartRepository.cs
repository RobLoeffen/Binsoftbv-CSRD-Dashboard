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

        public async Task<Ecopart?> GetByIdAsync(EcopartId id) => 
            await _context.Ecoparts.FirstOrDefaultAsync(e => e.Id == id);
    }
}
