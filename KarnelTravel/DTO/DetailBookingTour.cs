namespace KarnelTravel.DTO;

public class DetailBookingTour
{
    public int TourInvoiceId { get; set; }

    public int BookingId { get; set; }

    public float TourPrice { get; set; }

    public int NumOfPeople { get; set; }

    public float SubTotal { get; set; }

    public float Tax { get; set; }

    public string? DiscountCode { get; set; }

    public float? DiscountPercent { get; set; }

    public float Total { get; set; }
}
