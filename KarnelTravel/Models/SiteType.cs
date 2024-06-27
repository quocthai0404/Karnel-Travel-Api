using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class SiteType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Site> Sites { get; set; } = new List<Site>();
}
