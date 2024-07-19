using KarnelTravel.DTO;
using KarnelTravel.Models;
using System.Collections.Generic;

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

    public List<DetailBookingFlight> getAllDetail(List<int> bookingIdFlight)
    {
        var listDetailBookingFlight = new List<DetailBookingFlight>();
        foreach (var i in bookingIdFlight)
        {
            var item = db.FlightInvoices.Select(inv => new DetailBookingFlight()
            {
                FlightInvoiceId = inv.FlightInvoiceId,
                BookingId = inv.BookingId,
                FlightPrice = inv.FlightPrice,
                NumOfPassengers = inv.NumOfPassengers,
                SubTotal = inv.SubTotal,
                Tax = inv.Tax,
                DiscountCode = inv.DiscountCode,
                DiscountPercent = inv.DiscountPercent,
                Total = inv.Total

            }).FirstOrDefault(inv => inv.BookingId == i);
            if (item != null)
            {
                listDetailBookingFlight.Add(item);
            }

        }
        return listDetailBookingFlight;
    }
}
