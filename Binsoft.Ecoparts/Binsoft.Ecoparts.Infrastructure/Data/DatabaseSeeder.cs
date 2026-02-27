using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.ValueObjects;
using Binsoft.Ecoparts.Infrastructure.Persistances;
using Microsoft.EntityFrameworkCore;

namespace Binsoft.Ecoparts.Infrastructure.Data
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly AppDbContext _context;

        public DatabaseSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.Ecoparts.AnyAsync())
            {
                return;
            }

            var PET = new Material("PET", 1.27, 2.25);
            var HDPE = new Material("HDPE", 0.96, 3.09);
            var LDPE = new Material("LDPE", 0.94, 3.09);
            var PP = new Material("PP", 0.91, 3.09);
            var PS = new Material("PS", 1.06, 3.33);
            var PVC = new Material("PVC", 1.45, 1.39);
            var PLA = new Material("PLA", 1.24, 0.011);

            await _context.Materials.AddRangeAsync(PET, HDPE, LDPE, PP, PS, PVC, PLA);
            await _context.SaveChangesAsync();

            var ecoparts = new List<Ecopart>
            {
                new Ecopart("PET plate A", PET.Id, Shape.Rectangular(0.1, 0.1, 0.15)),
                new Ecopart("HDPE plate A", HDPE.Id, Shape.Cylinder(0.05, 0.2)),
                new Ecopart("LDPE plate A", LDPE.Id, Shape.Rectangular(0.1, 0.1, 0.15)),
                new Ecopart("PP plate A", PP.Id, Shape.Cylinder(0.05, 0.2)),
                new Ecopart("PS plate A", PS.Id, Shape.Rectangular(0.1, 0.1, 0.15)),
                new Ecopart("PVC plate A", PVC.Id, Shape.Cylinder(0.05, 0.2)),
                new Ecopart("PLA plate A", PLA.Id, Shape.Rectangular(0.1, 0.1, 0.15)),
            };

            await _context.Ecoparts.AddRangeAsync(ecoparts);
            await _context.SaveChangesAsync();
        }
    }
}
