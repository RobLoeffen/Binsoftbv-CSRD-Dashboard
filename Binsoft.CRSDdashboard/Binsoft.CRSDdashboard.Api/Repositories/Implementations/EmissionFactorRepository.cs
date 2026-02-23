using Binsoft.CRSDdashboard.Api.Data;
using Binsoft.CRSDdashboard.Api.Models.Entities;
using Binsoft.CRSDdashboard.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Binsoft.CRSDdashboard.Api.Repositories.Implementations
{
    public class EmissionFactorRepository : IEmissionFactorRepository
    {
        private readonly AppDbContext _context;

        public EmissionFactorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmissionFactor>> GetAllAsync()
        {
            return await _context.EmissionFactors
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<EmissionFactor?> GetByMaterialAsync(string material)
        {
            return await _context.EmissionFactors
                .AsNoTracking()
                .FirstOrDefaultAsync(ef => ef.Material == material);
        }
    }
}
