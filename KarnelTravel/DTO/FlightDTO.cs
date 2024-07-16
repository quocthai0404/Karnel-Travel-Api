namespace KarnelTravel.DTO;

public class FlightDTO
{
    public int FlightId { get; set; }

    public string DepartureAirportId { get; set; } = null!;

    public string ArrivalAirportId { get; set; } = null!;

    public string NameFlight { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public TimeOnly DepartureTime { get; set; }

    public TimeOnly ArrivalTime { get; set; }

    public float FlightPrice { get; set; }

    public bool IsHide { get; set; }
}
