using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Binsoft.Ecoparts.Infrastructure.Configurations
{
    internal class EcopartConfiguration : IEntityTypeConfiguration<Ecopart>
    {
        public void Configure(EntityTypeBuilder<Ecopart> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasConversion(
                id => id.Value,
                value => new EcopartId(value));

            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);

            builder.Property(e => e.MaterialId)
                .HasConversion(
                    id => id.Value,
                    value => new MaterialId(value))
                .IsRequired();

            builder.OwnsOne(e => e.Shape, shape =>
            {
                shape.Property(s => s.ShapeType)
                    .HasColumnName("ShapeType")
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();

                shape.Property(s => s.Height)
                    .HasColumnName("ShapeHeight");

                shape.Property(s => s.Length)
                    .HasColumnName("ShapeLength");

                shape.Property(s => s.Width)
                    .HasColumnName("ShapeWidth");

                shape.Property(s => s.Radius)
                    .HasColumnName("ShapeRadius");
            });
        }
    }
}
