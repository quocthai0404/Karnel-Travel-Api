using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class FlightInvoice
{
    public int FlightInvoiceId { get; set; }

    public int BookingId { get; set; }

    public double FlightPrice { get; set; }

    public int NumOfPassengers { get; set; }

    public double SubTotal { get; set; }

    public double Tax { get; set; }

    public string? DiscountCode { get; set; }

    public double? DiscountPercent { get; set; }

    public double Total { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
