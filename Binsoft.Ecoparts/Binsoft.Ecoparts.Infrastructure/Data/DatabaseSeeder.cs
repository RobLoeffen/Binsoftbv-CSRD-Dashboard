using Binsoft.Ecoparts.Infrastructure.Entities;
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
                return;

            var PET  = new Material { MaterialId = Guid.NewGuid(), MaterialName = "PET",  MaterialDensity = 1.27, MaterialEmissionFactor = 2.25 };
            var HDPE = new Material { MaterialId = Guid.NewGuid(), MaterialName = "HDPE", MaterialDensity = 0.96, MaterialEmissionFactor = 3.09 };
            var LDPE = new Material { MaterialId = Guid.NewGuid(), MaterialName = "LDPE", MaterialDensity = 0.94, MaterialEmissionFactor = 3.09 };
            var PP   = new Material { MaterialId = Guid.NewGuid(), MaterialName = "PP",   MaterialDensity = 0.91, MaterialEmissionFactor = 3.09 };
            var PS   = new Material { MaterialId = Guid.NewGuid(), MaterialName = "PS",   MaterialDensity = 1.06, MaterialEmissionFactor = 3.33 };
            var PVC  = new Material { MaterialId = Guid.NewGuid(), MaterialName = "PVC",  MaterialDensity = 1.45, MaterialEmissionFactor = 1.39 };
            var PLA  = new Material { MaterialId = Guid.NewGuid(), MaterialName = "PLA",  MaterialDensity = 1.24, MaterialEmissionFactor = 0.011 };

            await _context.Materials.AddRangeAsync(PET, HDPE, LDPE, PP, PS, PVC, PLA);
            await _context.SaveChangesAsync();

            var cylinderShape    = new Shape { ShapeId = Guid.NewGuid(), ShapeType = 0 };
            var rectangularShape = new Shape { ShapeId = Guid.NewGuid(), ShapeType = 1 };
            await _context.Shapes.AddRangeAsync(cylinderShape, rectangularShape);
            await _context.SaveChangesAsync();

            var ecoparts = new List<Ecopart>
            {
                new Ecopart { EcopartId = Guid.NewGuid(), EcopartName = "PET plate A",  MaterialId = PET.MaterialId,  ShapeId = cylinderShape.ShapeId,    DimHeight = 0.2, DimRadius = 0.05 },
                new Ecopart { EcopartId = Guid.NewGuid(), EcopartName = "PET plate B",  MaterialId = PET.MaterialId,  ShapeId = rectangularShape.ShapeId, DimHeight = 0.2, DimLength = 0.05, DimWidth = 0.8 },
                new Ecopart { EcopartId = Guid.NewGuid(), EcopartName = "HDPE plate A", MaterialId = HDPE.MaterialId, ShapeId = rectangularShape.ShapeId, DimHeight = 30,  DimLength = 10,   DimWidth = 20 },
                new Ecopart { EcopartId = Guid.NewGuid(), EcopartName = "LDPE plate A", MaterialId = LDPE.MaterialId, ShapeId = cylinderShape.ShapeId,    DimHeight = 0.2, DimRadius = 0.05 },
                new Ecopart { EcopartId = Guid.NewGuid(), EcopartName = "PP plate A",   MaterialId = PP.MaterialId,   ShapeId = cylinderShape.ShapeId,    DimHeight = 0.2, DimRadius = 0.05 },
                new Ecopart { EcopartId = Guid.NewGuid(), EcopartName = "PS plate A",   MaterialId = PS.MaterialId,   ShapeId = cylinderShape.ShapeId,    DimHeight = 0.2, DimRadius = 0.05 },
                new Ecopart { EcopartId = Guid.NewGuid(), EcopartName = "PVC plate A",  MaterialId = PVC.MaterialId,  ShapeId = cylinderShape.ShapeId,    DimHeight = 0.2, DimRadius = 0.05 },
                new Ecopart { EcopartId = Guid.NewGuid(), EcopartName = "PLA plate A",  MaterialId = PLA.MaterialId,  ShapeId = cylinderShape.ShapeId,    DimHeight = 0.2, DimRadius = 0.05 },
            };

            await _context.Ecoparts.AddRangeAsync(ecoparts);
            await _context.SaveChangesAsync();
        }
    }
}
