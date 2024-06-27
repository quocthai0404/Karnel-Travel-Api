using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class TourPersonQuantity
{
    public int TourId { get; set; }

    public int PerMax { get; set; }

    public int PerLeft { get; set; }
}
