using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class Flight
{
    public int FlightId { get; set; }

    public string DepartureAirportId { get; set; } = null!;

    public string ArrivalAirportId { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public TimeOnly DepartureTime { get; set; }

    public TimeOnly ArrivalTime { get; set; }

    public float FlightPrice { get; set; }

    public bool IsHide { get; set; }

    public virtual Airport ArrivalAirport { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Airport DepartureAirport { get; set; } = null!;
}
