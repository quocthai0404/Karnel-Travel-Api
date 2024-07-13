namespace KarnelTravel.DTO;

public class ReviewDto
{
    public int ReviewId { get; set; }

    public int ReviewStar { get; set; }

    public string ReviewText { get; set; } = null!;

    public int UserId { get; set; }

    public string UserFullName { get; set; }

    public int? HotelId { get; set; }

    public int? RestaurantId { get; set; }

    public bool IsHide { get; set; }
}
