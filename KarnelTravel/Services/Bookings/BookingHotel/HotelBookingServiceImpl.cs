using KarnelTravel.Models;

namespace KarnelTravel.Services.Bookings.BookingHotel;

public class HotelBookingServiceImpl : IHotelBookingService
{
    private DatabaseContext db; 
    public HotelBookingServiceImpl(DatabaseContext db)
    {
        this.db = db;
    }

    public bool AddBooking_Invoice_Hotel(Booking booking, HotelInvoice hotelInvoice)
    {
        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
               
                
                db.Bookings.Add(booking);
                db.SaveChanges(); 

                
                int bookingId = booking.BookingId;

                hotelInvoice.BookingId = bookingId;
                db.HotelInvoices.Add(hotelInvoice);
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
