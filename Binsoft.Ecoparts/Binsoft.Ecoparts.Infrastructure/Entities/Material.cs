using System;
using System.Collections.Generic;

namespace Binsoft.Ecoparts.Infrastructure.Entities;

public partial class Material
{
    public Guid MaterialId { get; set; }

    public string MaterialName { get; set; } = null!;

    public double MaterialDensity { get; set; }

    public double MaterialEmissionFactor { get; set; }

    public virtual ICollection<Ecopart> Ecoparts { get; set; } = new List<Ecopart>();
}
