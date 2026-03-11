using Microsoft.EntityFrameworkCore;
using Binsoft.Ecoparts.Infrastructure.Entities;

namespace Binsoft.Ecoparts.Infrastructure.Persistances;

public partial class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ecopart> Ecoparts { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Shape> Shapes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ecopart>(entity =>
        {
            entity.HasKey(e => e.EcopartId).HasName("PK__Ecoparts__1A2EB4E866FFBDC1");

            entity.Property(e => e.EcopartId).ValueGeneratedNever();
            entity.Property(e => e.EcopartName).HasMaxLength(200);

            entity.HasOne(d => d.Material).WithMany(p => p.Ecoparts)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ecoparts__Materi__4E88ABD4");

            entity.HasOne(d => d.Shape).WithMany(p => p.Ecoparts)
                .HasForeignKey(d => d.ShapeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ecoparts__ShapeI__4F7CD00D");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Material__C50610F7D1329F02");

            entity.Property(e => e.MaterialId).ValueGeneratedNever();
            entity.Property(e => e.MaterialName).HasMaxLength(200);
        });

        modelBuilder.Entity<Shape>(entity =>
        {
            entity.HasKey(e => e.ShapeId).HasName("PK__Shapes__70FC838177D96C46");

            entity.Property(e => e.ShapeId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
