namespace KarnelTravel.DTO;

public class BookingDTO
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int? FlightId { get; set; }

    public int? HotelId { get; set; }

    public int? TourId { get; set; }

    public string bookType { get; set; }
}
