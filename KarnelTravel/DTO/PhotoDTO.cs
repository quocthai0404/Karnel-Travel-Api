namespace KarnelTravel.DTO;

public class PhotoDTO
{
    public int PhotoId { get; set; }

    public string PhotoUrl { get; set; } = null!;

    public int? HotelId { get; set; }

    public int? RoomId { get; set; }

    public int? RestaurantId { get; set; }

    public int? BeachId { get; set; }

    public int? SiteId { get; set; }
}
