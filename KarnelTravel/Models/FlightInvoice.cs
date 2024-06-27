using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class FlightInvoice
{
    public int FlightInvoiceId { get; set; }

    public int BookingId { get; set; }

    public float FlightPrice { get; set; }

    public int NumOfPassengers { get; set; }

    public float SubTotal { get; set; }

    public float Tax { get; set; }

    public string? DiscountCode { get; set; }

    public float? DiscountPercent { get; set; }

    public float Total { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
