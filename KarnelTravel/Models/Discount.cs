using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class Discount
{
    public string? DiscountCode { get; set; }

    public float DiscountPercent { get; set; }
}
