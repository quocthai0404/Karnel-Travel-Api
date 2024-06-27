using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int? FlightId { get; set; }

    public int? HotelId { get; set; }

    public int? TourId { get; set; }

    public DateTime BookingDate { get; set; }

    public virtual Flight? Flight { get; set; }

    public virtual FlightInvoice? FlightInvoice { get; set; }

    public virtual Hotel? Hotel { get; set; }

    public virtual HotelInvoice? HotelInvoice { get; set; }

    public virtual Tour? Tour { get; set; }

    public virtual TourInvoice? TourInvoice { get; set; }

    public virtual User User { get; set; } = null!;
}
