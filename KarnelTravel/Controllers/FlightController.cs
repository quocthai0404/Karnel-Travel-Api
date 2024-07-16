using KarnelTravel.Services.Account;
using KarnelTravel.Services.Flights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarnelTravel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FlightController : ControllerBase
{
    private IFlightService flightService;
    public FlightController(IFlightService _flightService)
    {
        flightService = _flightService;
    }
    [HttpGet("getAllFlight")]
    public IActionResult getAll() {
        return Ok(flightService.getAllFlight());
    }
}
