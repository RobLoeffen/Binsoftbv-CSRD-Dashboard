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
        public DbSet<EmissionFactor> EmissionFactors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ecopart>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Material).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");
            });

            modelBuilder.Entity<EmissionFactor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Material).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Material).IsUnique();
            });

            modelBuilder.Entity<EmissionFactor>().HasData(
                new EmissionFactor { Id = 1, Material = "PET", Co2PerKg = 2.25 },
                new EmissionFactor { Id = 2, Material = "HDPE", Co2PerKg = 3.09 },
                new EmissionFactor { Id = 3, Material = "LDPE", Co2PerKg = 3.09 },
                new EmissionFactor { Id = 4, Material = "PP", Co2PerKg = 3.09 },
                new EmissionFactor { Id = 5, Material = "PS", Co2PerKg = 3.33 },
                new EmissionFactor { Id = 6, Material = "PVC", Co2PerKg = 1.39 },
                new EmissionFactor { Id = 7, Material = "PLA", Co2PerKg = 0.011 }
            );

            modelBuilder.Entity<Ecopart>().HasData(
                new Ecopart 
                { 
                    Id = 1, 
                    Name = "PET Plate", 
                    Length = 300, 
                    Width = 200, 
                    Height = 5,
                    Weight = 1.2, 
                    Material = "PET", 
                    CreatedAt = new DateTime(2024, 1, 15) 
                },
                new Ecopart 
                { 
                    Id = 2, 
                    Name = "HDPE Plate", 
                    Length = 300, 
                    Width = 200, 
                    Height = 5,
                    Weight = 1.5, 
                    Material = "HDPE", 
                    CreatedAt = new DateTime(2024, 1, 20) 
                },
                new Ecopart 
                { 
                    Id = 3, 
                    Name = "LDPE Plate", 
                    Length = 300, 
                    Width = 200, 
                    Height = 5,
                    Weight = 1.4, 
                    Material = "LDPE", 
                    CreatedAt = new DateTime(2024, 2, 5) 
                },
                new Ecopart 
                { 
                    Id = 4, 
                    Name = "PP Plate", 
                    Length = 300, 
                    Width = 200, 
                    Height = 5,
                    Weight = 1.3, 
                    Material = "PP", 
                    CreatedAt = new DateTime(2024, 2, 10) 
                },
                new Ecopart 
                { 
                    Id = 5, 
                    Name = "PS Plate", 
                    Length = 300, 
                    Width = 200, 
                    Height = 5,
                    Weight = 1.6, 
                    Material = "PS", 
                    CreatedAt = new DateTime(2024, 2, 15) 
                },
                new Ecopart 
                { 
                    Id = 6, 
                    Name = "PVC Plate", 
                    Length = 300, 
                    Width = 200, 
                    Height = 5,
                    Weight = 2.1, 
                    Material = "PVC", 
                    CreatedAt = new DateTime(2024, 2, 20) 
                },
                new Ecopart 
                { 
                    Id = 7, 
                    Name = "PLA Plate", 
                    Length = 300, 
                    Width = 200, 
                    Height = 5,
                    Weight = 1.8, 
                    Material = "PLA", 
                    CreatedAt = new DateTime(2024, 2, 25) 
                }
            );
        }
    }
}
