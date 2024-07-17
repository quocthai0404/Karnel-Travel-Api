using KarnelTravel.DTO;
using KarnelTravel.Models;
using KarnelTravel.Services.Account;
using KarnelTravel.Services.Bookings.BookingHotel;
using KarnelTravel.Services.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Globalization;

namespace KarnelTravel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private IHotelBookingService hotelBookingService;
    public BookingController(IHotelBookingService _hotelBookingService)
    {
        hotelBookingService = _hotelBookingService;
        
    }

    [Authorize]
    [HttpPost("addBookingHotel")]
    public IActionResult addBookingHotel([FromBody] BookingHotel booking) {
        DateTime checkInDate = DateTime.ParseExact(booking.checkin, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        DateTime checkOutDate = DateTime.ParseExact(booking.checkout, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null)
        {
            return BadRequest(new { Code = "400", Msg = "User ID not found" });
        }

        var bookingModel = new Booking()
        {
            UserId = int.Parse(userId),
            HotelId = booking.hotelid, 
            BookingDate = DateTime.Now
        };

        var hotelInvoice = new HotelInvoice()
        {
            RoomId = booking.roomid, 
            RoomPrice = booking.roomprice, 
            CheckinDate = checkInDate,
            CheckoutDate = checkOutDate,
            NumOfAdults = booking.numadult, 
            NumOfChildren = booking.numchild, 
            NumOfDays = booking.numday, 
            SubTotal = booking.subtotal,
            Tax = booking.tax,
            DiscountCode = booking.code, 
            DiscountPercent = booking.percent,
            Total = booking.total
        };
        if (hotelBookingService.AddBooking_Invoice_Hotel(bookingModel, hotelInvoice)) { 
            return Ok();
        }
        return BadRequest();
        
    }
}
