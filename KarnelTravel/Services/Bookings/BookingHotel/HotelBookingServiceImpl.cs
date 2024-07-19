using KarnelTravel.DTO;
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

    public List<DetailBookingHotel> getAllDetail(List<int> bookingIdHotel)
    {
        List<DetailBookingHotel> listDetailBookingHotel = new List<DetailBookingHotel>();
        foreach (int i in bookingIdHotel)
        {
            
            var invoice = db.HotelInvoices.SingleOrDefault(inv => (inv.BookingId == i));
            if (invoice != null)
            {
                try
                {
                    var item = new DetailBookingHotel()
                    {
                        HotelInvoiceId = invoice.HotelInvoiceId,
                        BookingId = invoice.BookingId,
                        RoomId = invoice.RoomId,
                        RoomPrice = invoice.RoomPrice,
                        CheckinDate = invoice.CheckinDate,
                        CheckoutDate = invoice.CheckoutDate,
                        NumOfAdults = invoice.NumOfAdults,
                        NumOfChildren = invoice.NumOfChildren,
                        NumOfDays = invoice.NumOfDays,
                        SubTotal = invoice.SubTotal,
                        Tax = invoice.Tax,
                        DiscountCode = invoice.DiscountCode,
                        DiscountPercent = invoice.DiscountPercent,
                        Total = invoice.Total,
                    };

                    listDetailBookingHotel.Add(item);
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine($"Invalid cast for BookingId: {i}");
                    Console.WriteLine($"RoomPrice: {invoice.RoomPrice.GetType()}");
                    Console.WriteLine($"SubTotal: {invoice.SubTotal.GetType()}");
                    Console.WriteLine($"Tax: {invoice.Tax.GetType()}");
                    Console.WriteLine($"DiscountPercent: {invoice.DiscountPercent.GetType()}");
                    Console.WriteLine($"Total: {invoice.Total.GetType()}");
                    Console.WriteLine(ex.Message);
                }
            }
        }
        return listDetailBookingHotel;
    }
}
