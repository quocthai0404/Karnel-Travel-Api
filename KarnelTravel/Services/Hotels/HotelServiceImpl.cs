using KarnelTravel.DTO;
using KarnelTravel.Models;
using KarnelTravel.Services.Facilities;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravel.Services.Hotels;

public class HotelServiceImpl : IHotelService
{
    private DatabaseContext db;
    private IFacilityService facilityService;
    public HotelServiceImpl(DatabaseContext _db, IFacilityService _facilityService)
    {
        db = _db;
        facilityService = _facilityService;
    }
    public List<HotelAndMainPhoto> listHotelsPaginated(int pageNumber, int pageSize)
    {
        var skipNumber = (pageNumber - 1) * pageSize;


        var hotelDTOs = findDTOsPaginated(skipNumber, pageSize);
        var hotelList = hotelDTOs.Select(hotelDTO =>
        {
            var hotelAndMainPhoto = new HotelAndMainPhoto()
            {
                HotelId = hotelDTO.HotelId,
                HotelName = hotelDTO.HotelName,
                HotelDescription = hotelDTO.HotelDescription,
                HotelPriceRange = hotelDTO.HotelPriceRange,
                HotelLocation = hotelDTO.HotelLocation,
                LocationId = hotelDTO.LocationId,
                IsHide = hotelDTO.IsHide, 
                facilities = facilityService.findAll(hotelDTO.HotelId)
            };

            var mainPhoto = findMainPhoto(hotelDTO.HotelId);
            if (mainPhoto != null)
            {
                hotelAndMainPhoto.PhotoUrl = mainPhoto.PhotoUrl;
            }

            return hotelAndMainPhoto;
        }).ToList();

        return hotelList;
    }

    public List<HotelDTO> findDTOsPaginated(int skipNumber, int pageSize)
    {
        return db.Hotels.Where(b => b.IsHide == false).Select(b => new HotelDTO
        {
            HotelId = b.HotelId,
            HotelName = b.HotelName,
            HotelDescription = b.HotelDescription,
            HotelPriceRange = b.HotelPriceRange,
            HotelLocation = b.HotelLocation,
            LocationId = b.LocationId,
            IsHide = b.IsHide
        }).Skip(skipNumber).Take(pageSize).ToList();
    }

    public List<PhotoDTO> findAllPhoto(int hotelId)
    {
        throw new NotImplementedException();
    }

    public HotelDTO findByIdDTO(int id)
    {
        throw new NotImplementedException();
    }

 
    public PhotoDTO findMainPhoto(int hotelId)
    {
        return db.Photos
            .Where(p => p.HotelId == hotelId)
            .Select(p => new PhotoDTO { PhotoUrl = p.PhotoUrl })
            .FirstOrDefault();
    }

    public int totalPages(int pageSize)
    {
        int totalItems = db.Hotels.Count(b => b.IsHide == false);
        int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        return totalPages;
    }
}
