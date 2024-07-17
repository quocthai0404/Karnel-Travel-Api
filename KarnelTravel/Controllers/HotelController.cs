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
        return Ok(new { 
            result = hotelService.listHotelsPaginated(query, pageSize),
            totalPages = hotelService.totalPages(query, pageSize), 
            pageSize = pageSize, 
            totalItem = hotelService.totalItem(query)
        }); 
    }

    [HttpGet("Hotel-Details/{hotelId}")]
    public IActionResult details(int hotelId)
    {
        return Ok(hotelService.HotelDetails(hotelId));
    }

    [HttpGet("findNameHotel/{hotelId}")]
    public IActionResult findNameHotel(int hotelId)
    {
        return Ok(new { name = hotelService.findHotelNameById(hotelId) });
    }

    [HttpGet("Room-Info/{roomId}")]
    public IActionResult Info(int roomId)
    {
        return Ok(hotelService.findRoom(roomId));
    }
}
