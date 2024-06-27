using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class Ward
{
    public int WardId { get; set; }

    public string WardName { get; set; } = null!;

    public int DistrictId { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<Street> Streets { get; set; } = new List<Street>();
}
