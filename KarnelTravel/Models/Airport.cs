using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class Airport
{
    public string AirportId { get; set; } = null!;

    public string AirportName { get; set; } = null!;

    public bool IsHide { get; set; }

    public virtual ICollection<Flight> FlightArrivalAirports { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightDepartureAirports { get; set; } = new List<Flight>();
}
