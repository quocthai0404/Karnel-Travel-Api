using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class Site
{
    public int SiteId { get; set; }

    public string SiteName { get; set; } = null!;

    public int LocationId { get; set; }

    public int TypeId { get; set; }

    public bool IsHide { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual SiteType Type { get; set; } = null!;
}
