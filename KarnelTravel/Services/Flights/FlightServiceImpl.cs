using KarnelTravel.DTO;
using KarnelTravel.Models;

namespace KarnelTravel.Services.Flights;

public class FlightServiceImpl : IFlightService
{
    private DatabaseContext db;
    public FlightServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }
    public List<FlightDTO> getAllFlight()
    {
        var flights = db.Flights.Where(f => f.IsHide == false && DateTime.Now <= f.StartDate)
            .Join(
            db.Airports,
            flight => flight.DepartureAirportId,
            airport => airport.AirportId,
            (flight, departureAirport) => new { flight, departureAirport }
            )
            .Join(
            db.Airports,
            combined => combined.flight.ArrivalAirportId,
            airport => airport.AirportId,
            (combined, arrivalAirport) => new FlightDTO
            {
                FlightId = combined.flight.FlightId,
                DepartureAirportId = combined.flight.DepartureAirportId,
                ArrivalAirportId = combined.flight.ArrivalAirportId,
                NameFlight = combined.departureAirport.AirportName + " " + "("+combined.flight.DepartureAirportId + ")" + " to " + arrivalAirport.AirportName + " " + "("+combined.flight.ArrivalAirportId+")",
                StartDate = combined.flight.StartDate,
                DepartureTime = combined.flight.DepartureTime,
                ArrivalTime = combined.flight.ArrivalTime,
                FlightPrice = combined.flight.FlightPrice,
                IsHide = combined.flight.IsHide
            }
        ).ToList();
        return flights;
        
    }
}
