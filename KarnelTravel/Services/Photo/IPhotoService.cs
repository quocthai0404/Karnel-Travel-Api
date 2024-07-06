using CloudinaryDotNet.Actions;

namespace KarnelTravel.Services.Photo;

public interface IPhotoService
{
    //public bool addPhoto(KarnelTravel.Models.Photo photo);
    //public bool addPhotos(List<KarnelTravel.Models.Photo> photos);

    //public void addPhoto(IFormFile file);
    //public void addPhotos(List<IFormFile> files);

    public ImageUploadResult AddPhoto(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}
