using KarnelTravel.Query;
using KarnelTravel.Services.Account;
using KarnelTravel.Services.Airport;
using KarnelTravel.Services.Flights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarnelTravel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FlightController : ControllerBase
{
    private IFlightService flightService;
    private IAirportService airportService;
    public FlightController(IFlightService _flightService, IAirportService _airportService)
    {
        flightService = _flightService;
        airportService = _airportService;
    }
    [HttpGet("getAllFlight")]
    public IActionResult getAll([FromQuery] QueryObject ob)
    {
        return Ok(flightService.getAllFlight(ob));
    }

    [HttpGet("getFlightById/{id}")]
    public IActionResult getFlightById(int id)
    {
        
        return Ok(flightService.GetFlightDTO(id));
    }

    [HttpGet("getAllAirport")]
    public IActionResult getAllAirport()
    {
        return Ok(airportService.findAll());
    }
}
