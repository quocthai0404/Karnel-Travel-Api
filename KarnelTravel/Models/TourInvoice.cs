using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class TourInvoice
{
    public int TourInvoiceId { get; set; }

    public int BookingId { get; set; }

    public double TourPrice { get; set; }

    public int NumOfPeople { get; set; }

    public double SubTotal { get; set; }

    public double Tax { get; set; }

    public string? DiscountCode { get; set; }

    public double? DiscountPercent { get; set; }

    public double Total { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
