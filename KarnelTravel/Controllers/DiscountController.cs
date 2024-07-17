using KarnelTravel.Services.Discounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarnelTravel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private IDiscountService discountService;
    public DiscountController(IDiscountService _discountService) { 
        discountService = _discountService;
    }
    [HttpGet("getPercent")]
    public IActionResult getPercent(string code) { 
        return Ok(discountService.getPercent(code));
    }
}
