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

            builder.Property(e => e.ShapeId)
                .HasConversion(
                    id => id.Value,
                    value => new ShapeId(value))
                .IsRequired();

            builder.OwnsOne(e => e.Dimension, d =>
            {
                d.Property(dim => dim.Height).HasColumnName("DimHeight").IsRequired();
                d.Property(dim => dim.Length).HasColumnName("DimLength").IsRequired(false);
                d.Property(dim => dim.Width).HasColumnName("DimWidth").IsRequired(false);
                d.Property(dim => dim.Radius).HasColumnName("DimRadius").IsRequired(false);
            });
        }
    }
}
