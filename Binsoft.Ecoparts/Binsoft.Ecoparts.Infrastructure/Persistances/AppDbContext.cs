using Binsoft.Ecoparts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Binsoft.Ecoparts.Infrastructure.Persistances
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Ecopart> Ecoparts { get; set; } = null!;
        public DbSet<Material> Materials { get; set; } = null!;
        public DbSet<Shape> Shapes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
