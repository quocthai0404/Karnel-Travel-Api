using KarnelTravel.Models;

namespace KarnelTravel.Services.Bookings.BookingFlight;

public class FlightBookingServiceImpl : IFlightBookingService
{
    private DatabaseContext db;
    public FlightBookingServiceImpl(DatabaseContext db)
    {
        this.db = db;
    }
    public bool AddBooking_Invoice_Flight(Booking booking, FlightInvoice flightInvoice)
    {
        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                db.Bookings.Add(booking);
                db.SaveChanges();


                int bookingId = booking.BookingId;

                flightInvoice.BookingId = bookingId;
                db.FlightInvoices.Add(flightInvoice);
                db.SaveChanges();


                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {

                transaction.Rollback();
                Console.WriteLine($"err: {ex.Message}");
                return false;
            }
        }
    }
}
