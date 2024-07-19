using CloudinaryDotNet.Actions;
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
            RoomPrice = (float)booking.roomprice, 
            CheckinDate = checkInDate,
            CheckoutDate = checkOutDate,
            NumOfAdults = booking.numadult, 
            NumOfChildren = booking.numchild, 
            NumOfDays = booking.numday, 
            SubTotal = (float)booking.subtotal,
            Tax = (float)booking.tax,
            DiscountCode = booking.code, 
            DiscountPercent = (float)booking.percent,
            Total = (float)booking.total
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
            FlightPrice = (float)bookingObject.flightprice,
            NumOfPassengers = bookingObject.numofpass,
            SubTotal = (float)bookingObject.subtotal,
            Tax = (float)bookingObject.tax,
            DiscountCode = bookingObject.code,
            DiscountPercent = (float)bookingObject.percent,
            Total = (float)bookingObject.total
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
            TourPrice = (float)bookingObject.tourprice,
            NumOfPeople = bookingObject.numofpeo,
            SubTotal = (float)bookingObject.subtotal,
            Tax = (float)bookingObject.tax,
            DiscountCode = bookingObject.code,
            DiscountPercent = (float)bookingObject.percent,
            Total = (float)bookingObject.total
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

        //list booking id 
        List<int> bookingIdHotel = new List<int>();
        List<int> bookingIdFlight = new List<int>();
        List<int> bookingIdTour = new List<int>();
        List<DetailBookingHotel> listDetailBookingHotel = new List<DetailBookingHotel>();
        List<DetailBookingFlight> listDetailBookingFlight = new List<DetailBookingFlight>();
        List<DetailBookingTour> listDetailBookingTour = new List<DetailBookingTour>();

        foreach (var i in list) {


            if (i.FlightId.HasValue)
            {
                var flightInvoice = (from b in db.Bookings
                                     join fi in db.FlightInvoices on b.BookingId equals fi.BookingId
                                     where b.BookingId == i.BookingId
                                     select new DetailBookingFlight
                                     {
                                         BookingId = b.BookingId,
                                         FlightInvoiceId = fi.FlightInvoiceId,
                                         FlightPrice = fi.FlightPrice,
                                         NumOfPassengers = fi.NumOfPassengers,
                                         SubTotal = fi.SubTotal,
                                         Tax = fi.Tax,
                                         DiscountCode = fi.DiscountCode,
                                         DiscountPercent = fi.DiscountPercent,
                                         Total = fi.Total
                                     }).FirstOrDefault();

                if (flightInvoice != null)
                {
                    listDetailBookingFlight.Add(flightInvoice);
                }
                    

            }

            if (i.HotelId.HasValue)
            {
                var HotelInvoice = (from b in db.Bookings
                                     join hi in db.HotelInvoices on b.BookingId equals hi.BookingId
                                     where b.BookingId == i.BookingId
                                     select new DetailBookingHotel
                                     {
                                         BookingId = b.BookingId,
                                         HotelInvoiceId = hi.HotelInvoiceId,
                                         RoomId = hi.RoomId,
                                         RoomPrice = hi.RoomPrice,
                                         CheckinDate =hi.CheckinDate,
                                         CheckoutDate = hi.CheckoutDate,
                                         NumOfAdults = hi.NumOfAdults,
                                         NumOfChildren = hi.NumOfChildren,
                                         NumOfDays = hi.NumOfDays, 

                                        
                                         SubTotal = hi.SubTotal,
                                         Tax = hi.Tax,
                                         DiscountCode = hi.DiscountCode,
                                         DiscountPercent = hi.DiscountPercent,
                                         Total = hi.Total
                                     }).FirstOrDefault();

                if (HotelInvoice != null)
                {
                    listDetailBookingHotel.Add(HotelInvoice);
                }


            }

            if (i.TourId.HasValue)
            {
                var tourInvoice = (from b in db.Bookings
                                    join ti in db.TourInvoices on b.BookingId equals ti.BookingId
                                    where b.BookingId == i.BookingId
                                    select new DetailBookingTour
                                    {
                                        BookingId = b.BookingId,
                                        TourInvoiceId = ti.TourInvoiceId,
                                        TourPrice = ti.TourPrice,
                                        NumOfPeople = ti.NumOfPeople, 


                                        SubTotal = ti.SubTotal,
                                        Tax = ti.Tax,
                                        DiscountCode = ti.DiscountCode,
                                        DiscountPercent = ti.DiscountPercent,
                                        Total = ti.Total
                                    }).FirstOrDefault();

                if (tourInvoice != null)
                {
                    listDetailBookingTour.Add(tourInvoice);
                }


            }
        }


        return Ok(new
        {
            listDetailBookingFlight = listDetailBookingFlight,
            listDetailBookingHotel = listDetailBookingHotel,
            listDetailBookingTour = listDetailBookingTour
        }) ;
    }

    
}
