using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Binsoft.Ecoparts.Infrastructure.Configurations;

internal class ShapeConfiguration : IEntityTypeConfiguration<Shape>
{
    public void Configure(EntityTypeBuilder<Shape> builder)
    {
        builder.ToTable("Shapes");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasConversion(
            id => id.Value,
            value => new ShapeId(value));
        builder.Property(s => s.ShapeType).HasConversion<string>().HasMaxLength(50);
    }
}
