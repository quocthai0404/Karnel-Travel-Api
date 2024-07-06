using KarnelTravel.DTO;
using KarnelTravel.Services.Beach;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarnelTravel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BeachController : ControllerBase
{
    private IBeachService beachService;
    private IWebHostEnvironment webHostEnvironment;
    private IConfiguration configuration;
    public BeachController(IBeachService _beachService, IWebHostEnvironment _webHostEnvironment, IConfiguration _configuration)
    {
        beachService = _beachService;
        webHostEnvironment = webHostEnvironment;
        configuration = _configuration;
    }

    [HttpPost("addBeach")]
    public IActionResult AddBeach([FromBody] BeachDTO beach)
    {
        if (beachService.create(new Models.Beach { BeachName = beach.BeachName, BeachLocation = beach.BeachLocation, LocationId = beach.LocationId })) {
            return Ok(new {result = "add beach ok" });
        }
        return BadRequest(new { result = "add beach failed" });

    }
}
