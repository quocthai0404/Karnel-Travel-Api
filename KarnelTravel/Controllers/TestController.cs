using KarnelTravel.Services.Airport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarnelTravel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private IAirportService airportService;
    public TestController(IAirportService _airportService) { 
        airportService = _airportService;
    }

    [HttpGet("getAirportById")]
    public IActionResult getById(string id) {
        return Ok(airportService.findByIdDTO(id));
    }

    [HttpGet("deleteById")]
    public IActionResult Delete(string id)
    {
        return Ok( new { result = airportService.delete(id) });
    }

    [HttpGet("findall")]
    public IActionResult findall()
    {
        return Ok(new { result = airportService.findAll() });
    }

    [HttpGet("findAllDeleted")]
    public IActionResult findAllDeleted()
    {
        return Ok(new { result = airportService.findAllDeleted() });
    }

    [HttpGet("Recover")]
    public IActionResult Recover(string airportId)
    {
        return Ok(new { result = airportService.Recover(airportId) });
    }
}
