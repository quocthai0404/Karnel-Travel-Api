using KarnelTravel.DTO;
using KarnelTravel.Models;
using KarnelTravel.Services.Account;
using KarnelTravel.Services.Mail;
using KarnelTravel.Services.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarnelTravel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    
    private IReviewService reviewService;
    public ReviewController(IReviewService _reviewService)
    {
        reviewService = _reviewService;
    }
    [Authorize]
    [HttpPost("addReview")]
    public IActionResult addReview([FromBody] ReviewUserPost reviewDto) {
        var userId = User.FindFirst("UserId")?.Value;
        if (userId == null)
        {
            return BadRequest(new { Code = "400", Msg = "User ID not found" });
        }

        var review = new Review
        {
            ReviewStar = int.Parse(reviewDto.reviewStar),
            ReviewText = reviewDto.reviewText,
            UserId = int.Parse(userId),
            HotelId = reviewDto.hotelId

        };
        if (reviewService.addReview(review)) { 
            return Ok();
        }
        return BadRequest();
    }
}
