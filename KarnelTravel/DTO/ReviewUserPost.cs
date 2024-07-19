using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KarnelTravel.DTO;

public class ReviewUserPost
{
    public int reviewId { get; set; }
    public string reviewStar { get; set; }
    public string reviewText { get; set; }
    public int userId { get; set; }
    public string userFullName { get; set; }
    public int hotelId { get; set; }
    public int restaurantId { get; set; }
    public string isHide { get; set; }
}
