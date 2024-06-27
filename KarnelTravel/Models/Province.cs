using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class Province
{
    public int ProvinceId { get; set; }

    public string ProvinceName { get; set; } = null!;

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<Tour> TourArrivalNavigations { get; set; } = new List<Tour>();

    public virtual ICollection<Tour> TourDepartureNavigations { get; set; } = new List<Tour>();
}
