using KarnelTravel.DTO;
using KarnelTravel.Models;

namespace KarnelTravel.Services.Bookings.BookingTour;

public interface IBookingTourService
{
    public bool AddBooking_Invoice_Tour(Booking booking, TourInvoice tourInvoice);
    public List<DetailBookingTour> getAllDetail(List<int> bookingIdTour);
}
