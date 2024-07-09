namespace KarnelTravel.DTO;

public class HotelDTO
{
    public int HotelId { get; set; }

    public string HotelName { get; set; } = null!;

    public string? HotelDescription { get; set; }

    public string? HotelPriceRange { get; set; }

    public string? HotelLocation { get; set; }

    public int? LocationId { get; set; }

    public bool IsHide { get; set; }
}
