using KarnelTravel.DTO;
using KarnelTravel.Models;

namespace KarnelTravel.Services.Bookings.BookingTour;

public class BookingTourServiceImpl : IBookingTourService
{
    private DatabaseContext db;
    public BookingTourServiceImpl(DatabaseContext db)
    {
        this.db = db;
    }
    public bool AddBooking_Invoice_Tour(Booking booking, TourInvoice tourInvoice)
    {
        using (var transaction = db.Database.BeginTransaction())
        {
            try
            {
                db.Bookings.Add(booking);
                db.SaveChanges();


                int bookingId = booking.BookingId;

                tourInvoice.BookingId = bookingId;
                db.TourInvoices.Add(tourInvoice);
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

    public List<DetailBookingTour> getAllDetail(List<int> bookingIdTour)
    {
        List<DetailBookingTour> listDetailBookingTour = new List<DetailBookingTour>();
        foreach (var i in bookingIdTour)
        {
            var item = db.TourInvoices.Select(inv => new DetailBookingTour()
            {
                TourInvoiceId = inv.TourInvoiceId,
                BookingId = inv.BookingId,
                TourPrice = inv.TourPrice,
                NumOfPeople = inv.NumOfPeople,
                SubTotal = inv.SubTotal,
                Tax = inv.Tax,
                DiscountCode = inv.DiscountCode,
                DiscountPercent = inv.DiscountPercent,
                Total = inv.Total

            }).FirstOrDefault(inv => inv.BookingId == i);
            if (item != null)
            {
                listDetailBookingTour.Add(item);
            }

        }
        return listDetailBookingTour;
    }
}
