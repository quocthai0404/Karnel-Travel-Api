using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class Facility
{
    public int FacilityId { get; set; }

    public int FacilityName { get; set; }

    public bool IsHide { get; set; }

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}
