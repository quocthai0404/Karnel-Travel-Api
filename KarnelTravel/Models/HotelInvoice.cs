using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class HotelInvoice
{
    public int HotelInvoiceId { get; set; }

    public int BookingId { get; set; }

    public int RoomId { get; set; }

    public double RoomPrice { get; set; }

    public DateTime CheckinDate { get; set; }

    public DateTime CheckoutDate { get; set; }

    public int NumOfAdults { get; set; }

    public int NumOfChildren { get; set; }

    public int NumOfDays { get; set; }

    public double SubTotal { get; set; }

    public double Tax { get; set; }

    public string? DiscountCode { get; set; }

    public double DiscountPercent { get; set; }

    public double Total { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
