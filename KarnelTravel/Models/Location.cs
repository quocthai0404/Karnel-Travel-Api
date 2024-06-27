using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string? LocationNumber { get; set; }

    public int ProvinceId { get; set; }

    public int DistrictId { get; set; }

    public int WardId { get; set; }

    public int StreetId { get; set; }

    public bool IsHide { get; set; }

    public virtual ICollection<Beach> Beaches { get; set; } = new List<Beach>();

    public virtual District District { get; set; } = null!;

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();

    public virtual Province Province { get; set; } = null!;

    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

    public virtual ICollection<Site> Sites { get; set; } = new List<Site>();

    public virtual Street Street { get; set; } = null!;

    public virtual Ward Ward { get; set; } = null!;
}
