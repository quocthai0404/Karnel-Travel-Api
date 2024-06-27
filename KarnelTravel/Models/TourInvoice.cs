using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class TourInvoice
{
    public int TourInvoiceId { get; set; }

    public int BookingId { get; set; }

    public float TourPrice { get; set; }

    public int NumOfPeople { get; set; }

    public float SubTotal { get; set; }

    public float Tax { get; set; }

    public string? DiscountCode { get; set; }

    public float? DiscountPercent { get; set; }

    public float Total { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
