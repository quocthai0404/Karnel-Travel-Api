
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet.Provisioning;
using dotenv.net;
using KarnelTravel.Helpers;
using KarnelTravel.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace KarnelTravel.Services.Photo;

public class PhotoServiceImpl : IPhotoService
{
    private DatabaseContext db;
    private Cloudinary cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
    

    public PhotoServiceImpl(DatabaseContext _db)
    {
        cloudinary.Api.Secure = true;
        db = _db;
    }

    

    public ImageUploadResult AddPhoto(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();
        if (file.Length > 0) { 
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream)
            };
            uploadResult = cloudinary.Upload(uploadParams);
        }
        return uploadResult;
    }

    
    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = await cloudinary.DestroyAsync(deleteParams); 
        return result;
    }
}
