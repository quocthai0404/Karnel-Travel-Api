using KarnelTravel.DTO;
using KarnelTravel.Models;

namespace KarnelTravel.Services.Bookings.BookingFlight;

public interface IFlightBookingService
{
    public bool AddBooking_Invoice_Flight(Booking booking, FlightInvoice flightInvoice);
    public List<DetailBookingFlight> getAllDetail(List<int> bookingIdFlight);
}
