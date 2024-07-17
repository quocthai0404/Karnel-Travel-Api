using KarnelTravel.Models;

namespace KarnelTravel.Services.Bookings.BookingHotel;

public interface IHotelBookingService
{
    public bool AddBooking_Invoice_Hotel(Booking booking, HotelInvoice hotelInvoice);
}
