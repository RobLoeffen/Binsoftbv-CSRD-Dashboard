using System;
using System.Collections.Generic;

namespace Binsoft.Ecoparts.Infrastructure.Entities;

public partial class Shape
{
    public Guid ShapeId { get; set; }

    public int ShapeType { get; set; }

    public virtual ICollection<Ecopart> Ecoparts { get; set; } = new List<Ecopart>();
}
