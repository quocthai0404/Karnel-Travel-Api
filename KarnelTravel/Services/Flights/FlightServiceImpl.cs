using KarnelTravel.DTO;
using KarnelTravel.Models;
using KarnelTravel.Query;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KarnelTravel.Services.Flights;

public class FlightServiceImpl : IFlightService
{
    private DatabaseContext db;
    public FlightServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }
    public List<FlightDTO> getAllFlight(QueryObject ob)
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
                NameFlight = combined.departureAirport.AirportName + " " + "(" + combined.flight.DepartureAirportId + ")" + " to " + arrivalAirport.AirportName + " " + "(" + combined.flight.ArrivalAirportId + ")",
                StartDate = combined.flight.StartDate,
                DepartureTime = combined.flight.DepartureTime,
                ArrivalTime = combined.flight.ArrivalTime,
                FlightPrice = combined.flight.FlightPrice,
                IsHide = combined.flight.IsHide
            }
        );

        if (!string.IsNullOrEmpty(ob.from))
        {
            flights = flights.Where(f => f.DepartureAirportId == ob.from);
        }

        if (!string.IsNullOrEmpty(ob.to))
        {
            flights = flights.Where(f => f.ArrivalAirportId == ob.to);
        }

        if (!string.IsNullOrEmpty(ob.date))
        {
            if (DateTime.TryParse(ob.date, out DateTime parsedDate))
            {
                flights = flights.Where(f => f.StartDate.Date == parsedDate.Date);
            }
        }

        
        if (!string.IsNullOrEmpty(ob.price))
        {
            if (ob.price == "hightolow")
            {
                flights = flights.OrderByDescending(f => f.FlightPrice);
            }
            else if (ob.price == "lowtohigh")
            {
                flights = flights.OrderBy(f => f.FlightPrice);
            }
        }

        
        
        return flights.ToList();

    }

    public FlightDTO GetFlightDTO(int id)
    {
        return db.Flights
        .Where(f => f.FlightId == id && f.IsHide == false)
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
                NameFlight = combined.departureAirport.AirportName + " " + "(" + combined.flight.DepartureAirportId + ")" + " to " + arrivalAirport.AirportName + " " + "(" + combined.flight.ArrivalAirportId + ")",
                StartDate = combined.flight.StartDate,
                DepartureTime = combined.flight.DepartureTime,
                ArrivalTime = combined.flight.ArrivalTime,
                FlightPrice = combined.flight.FlightPrice,
                IsHide = combined.flight.IsHide
            })
        .FirstOrDefault();
    }
}
