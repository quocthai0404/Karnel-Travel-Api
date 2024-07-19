using KarnelTravel.DTO;
using KarnelTravel.Models;
using KarnelTravel.Services.Account;
using KarnelTravel.Services.Bookings.BookingFlight;
using KarnelTravel.Services.Bookings.BookingHotel;
using KarnelTravel.Services.Bookings.BookingTour;
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
    private IFlightBookingService flightBookingService;
    private IBookingTourService tourBookingService;

    private DatabaseContext db;
    public BookingController(IHotelBookingService _hotelBookingService, IFlightBookingService _flightBookingService, IBookingTourService _tourBookingService, DatabaseContext _db)
    {
        hotelBookingService = _hotelBookingService;
        flightBookingService = _flightBookingService;
        tourBookingService = _tourBookingService;
        db = _db;


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

    [Authorize]
    [HttpPost("addBookingFlight")]
    public IActionResult addBookingFlight([FromBody] BookingFlight bookingObject)
    {
        

        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null)
        {
            return BadRequest(new { Code = "400", Msg = "User ID not found" });
        }

        var bookingModel = new Booking()
        {
            UserId = int.Parse(userId),
            FlightId = bookingObject.flightid,
            BookingDate = DateTime.Now
        };

        var flightInvoice = new FlightInvoice()
        {
            FlightPrice = bookingObject.flightprice,
            NumOfPassengers = bookingObject.numofpass,
            SubTotal = bookingObject.subtotal,
            Tax = bookingObject.tax,
            DiscountCode = bookingObject.code,
            DiscountPercent = bookingObject.percent,
            Total = bookingObject.total
        };
        if (flightBookingService.AddBooking_Invoice_Flight(bookingModel, flightInvoice))
        {
            return Ok();
        }
        return BadRequest();

    }

    [Authorize]
    [HttpPost("addBookingTour")]
    public IActionResult addBookingTour([FromBody] BookingTour bookingObject)
    {


        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null)
        {
            return BadRequest(new { Code = "400", Msg = "User ID not found" });
        }

        var bookingModel = new Booking()
        {
            UserId = int.Parse(userId),
            TourId = bookingObject.tourid,
            BookingDate = DateTime.Now
        };

        var tourInvoice = new TourInvoice()
        {
            TourPrice = bookingObject.tourprice,
            NumOfPeople = bookingObject.numofpeo,
            SubTotal = bookingObject.subtotal,
            Tax = bookingObject.tax,
            DiscountCode = bookingObject.code,
            DiscountPercent = bookingObject.percent,
            Total = bookingObject.total
        };
        if (tourBookingService.AddBooking_Invoice_Tour(bookingModel, tourInvoice))
        {
            return Ok();
        }
        return BadRequest();

    }

    [Authorize]
    [HttpGet("getAllBooking")]
    public IActionResult getAllBooking() {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null)
        {
            return BadRequest(new { Code = "400", Msg = "User ID not found" });
        }

        var u_id = int.Parse(userId);
        var list = new List<BookingDTO>();
        list = db.Bookings.Where(b => b.UserId == u_id).Select(b => new BookingDTO()
        {
            BookingId = b.BookingId,
            UserId = b.UserId,
            FlightId = b.FlightId,
            HotelId = b.HotelId,
            TourId = b.TourId
        }).ToList();
        foreach (var i in list) {
            if (i.FlightId != null) {
                i.bookType = "flight";
            }
            if (i.HotelId != null)
            {
                i.bookType = "hotel";
            }
            if (i.TourId != null)
            {
                i.bookType = "tour";
            }
        }
        return Ok(list);
    }

    
}
