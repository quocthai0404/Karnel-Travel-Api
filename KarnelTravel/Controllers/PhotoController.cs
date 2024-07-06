using KarnelTravel.DTO;
using KarnelTravel.Models;
using KarnelTravel.Services.Photo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarnelTravel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PhotoController : ControllerBase
{
    private IPhotoService photoService;
    public PhotoController(IPhotoService _photoService) {
        photoService = _photoService;
    }
    [HttpPost("addPhoto")]
    public IActionResult addPhoto(IFormFile file)
    {

        return Ok(photoService.AddPhoto(file).Url);
        
    }

    
}
