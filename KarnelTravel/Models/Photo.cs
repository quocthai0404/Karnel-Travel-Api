using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class Photo
{
    public int PhotoId { get; set; }

    public string PhotoUrl { get; set; } = null!;

    public int? HotelId { get; set; }

    public int? RoomId { get; set; }

    public int? RestaurantId { get; set; }

    public int? BeachId { get; set; }

    public int? SiteId { get; set; }

    public virtual Beach? Beach { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual Restaurant? Restaurant { get; set; }

    public virtual Room? Room { get; set; }

    public virtual Site? Site { get; set; }
}
