using Binsoft.CRSDdashboard.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Binsoft.CRSDdashboard.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Ecopart> Ecoparts { get; set; }
        public DbSet<Material> Material { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ecopart>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.HasOne(e => e.Material)
                      .WithMany(m => m.Ecoparts)
                      .HasForeignKey(e => e.MaterialId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Material>().HasData(
                new Material { Id = 1, Name = "PET", Co2PerKg = 2.25, Density = 1.27 },
                new Material { Id = 2, Name = "HDPE", Co2PerKg = 3.09, Density = 0.96 },
                new Material { Id = 3, Name = "LDPE", Co2PerKg = 3.09, Density = 0.94 },
                new Material { Id = 4, Name = "PP", Co2PerKg = 3.09, Density = 0.91 },
                new Material { Id = 5, Name = "PS", Co2PerKg = 3.33, Density = 1.06 },
                new Material { Id = 6, Name = "PVC", Co2PerKg = 1.39, Density = 1.45 },
                new Material { Id = 7, Name = "PLA", Co2PerKg = 0.011, Density = 1.24 }
            );

            modelBuilder.Entity<Ecopart>().HasData(
                new Ecopart 
                { 
                    Id = 1, 
                    Name = "PET Plate", 
                    ShapeType = ShapeType.RectangularPrism,
                    Length = 3, 
                    Width = 2, 
                    Height = 5,
                    MaterialId = 1, 
                    CreatedAt = new DateTime(2024, 1, 15) 
                },
                new Ecopart 
                { 
                    Id = 2, 
                    Name = "HDPE Plate", 
                    ShapeType = ShapeType.RectangularPrism,
                    Length = 1, 
                    Width = .5, 
                    Height = 5,
                    MaterialId = 2,
                    CreatedAt = new DateTime(2024, 1, 20) 
                },
                new Ecopart 
                { 
                    Id = 3, 
                    Name = "LDPE Plate", 
                    ShapeType = ShapeType.RectangularPrism,
                    Length = 4, 
                    Width = 6, 
                    Height = 5,
                    MaterialId = 3,
                    CreatedAt = new DateTime(2024, 2, 5) 
                },
                new Ecopart 
                { 
                    Id = 4, 
                    Name = "PP Plate", 
                    ShapeType = ShapeType.RectangularPrism,
                    Length = .1, 
                    Width = .5, 
                    Height = 5,
                    MaterialId = 4,
                    CreatedAt = new DateTime(2024, 2, 10) 
                },
                new Ecopart 
                { 
                    Id = 5, 
                    Name = "PS Plate", 
                    ShapeType = ShapeType.RectangularPrism,
                    Length = 3, 
                    Width = 2, 
                    Height = 5,
                    MaterialId = 5,
                    CreatedAt = new DateTime(2024, 2, 15) 
                },
                new Ecopart 
                { 
                    Id = 6, 
                    Name = "PVC Plate", 
                    ShapeType = ShapeType.RectangularPrism,
                    Length = 10, 
                    Width = 20, 
                    Height = 5,
                    MaterialId = 6,
                    CreatedAt = new DateTime(2024, 2, 20) 
                },
                new Ecopart 
                { 
                    Id = 7, 
                    Name = "PLA Plate", 
                    ShapeType = ShapeType.RectangularPrism,
                    Length = 1, 
                    Width = 1, 
                    Height = 5,
                    MaterialId = 7,
                    CreatedAt = new DateTime(2024, 2, 25) 
                }
            );
        }
    }
}
