using KarnelTravel.Services.Photo;
using KarnelTravel.Services.Tours;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarnelTravel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TourController : ControllerBase
{
    private ITourService tourService;
    public TourController(ITourService _tourService)
    {
        tourService = _tourService;
    }
    [HttpGet("getAllTour")]
    public IActionResult getAllTour()
    {

        return Ok(tourService.findAllTour());

    }

    [HttpGet("getTourDetail/{id}")]
    public IActionResult getAllTour(int id)
    {

        return Ok(tourService.findById(id));

    }
}
