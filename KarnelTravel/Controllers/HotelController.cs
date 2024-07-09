using KarnelTravel.Query;
using KarnelTravel.Services.Hotels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarnelTravel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HotelController : ControllerBase
{
    private int pageSize = 8;
    private IHotelService hotelService;
    public HotelController(IHotelService _hotelService)
    {
        hotelService = _hotelService;
    }
    [HttpGet("HotelsPaginated")]
    public IActionResult getHotelsPaginated([FromQuery] QueryObject query) {
        return Ok(new { result = hotelService.listHotelsPaginated(query.PageNumber, pageSize),
            totalPages = hotelService.totalPages(pageSize), 
            pageSize = pageSize
        }); 
    }
}
