using Binsoft.Ecoparts.Domain.Entities;
using Binsoft.Ecoparts.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Binsoft.Ecoparts.Infrastructure.Configurations;

internal class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.ToTable("Materials");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasConversion(
            id => id.Value,
            value => new MaterialId(value));

        builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
        builder.Property(m => m.Density).IsRequired();
        builder.Property(m => m.EmissionFactor).IsRequired();
    }
}
