using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class Site
{
    public int SiteId { get; set; }

    public string SiteName { get; set; } = null!;

    public string? SiteLocation { get; set; }

    public int? LocationId { get; set; }

    public int TypeId { get; set; }

    public bool IsHide { get; set; }

    public virtual Location? Location { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual SiteType Type { get; set; } = null!;
}
