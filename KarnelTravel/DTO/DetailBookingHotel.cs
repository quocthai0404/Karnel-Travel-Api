namespace KarnelTravel.DTO;

public class DetailBookingHotel
{
    public int HotelInvoiceId { get; set; }

    public int BookingId { get; set; }

    public int RoomId { get; set; }

    public float RoomPrice { get; set; }

    public DateTime CheckinDate { get; set; }

    public DateTime CheckoutDate { get; set; }

    public int NumOfAdults { get; set; }

    public int NumOfChildren { get; set; }

    public int NumOfDays { get; set; }

    public float SubTotal { get; set; }

    public float Tax { get; set; }

    public string? DiscountCode { get; set; }

    public float DiscountPercent { get; set; }

    public float Total { get; set; }
}
