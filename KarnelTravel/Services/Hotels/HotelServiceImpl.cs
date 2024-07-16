using KarnelTravel.DTO;
using KarnelTravel.Models;
using KarnelTravel.Query;
using KarnelTravel.Services.Facilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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
            var reviews = findAllReview(hotelDTO.HotelId);
            var countReview = reviews.Count();
            var totalStar = getSumOfReviewStars(hotelDTO.HotelId);
            double star = 0;
            if (countReview != 0)
            {
                star = (double)totalStar / (double)countReview;
            }
            var hotelAndMainPhoto = new HotelAndMainPhoto()
            {
                HotelId = hotelDTO.HotelId,
                HotelName = hotelDTO.HotelName,
                HotelDescription = hotelDTO.HotelDescription,
                HotelPriceRange = hotelDTO.HotelPriceRange,
                HotelLocation = hotelDTO.HotelLocation,
                LocationId = hotelDTO.LocationId,
                IsHide = hotelDTO.IsHide,
                countReview = countReview,
                totalStar = totalStar,
                star = Math.Round(star, 1),
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

    public List<PhotoDTO> findAllPhoto(int hotelId, int n)
    {
        return db.Photos.Where(p => p.HotelId == hotelId).Select(p => new PhotoDTO {
            PhotoId = p.PhotoId,
            PhotoUrl = p.PhotoUrl,
            HotelId = p.HotelId,
        }).Take(n).ToList();
    }

    public dynamic HotelDetails(int id)
    {
        var facilities = facilityService.findAll(id);
        var hotel = db.Hotels
            .Select(h => new HotelAndMainPhoto()
            {
                HotelId = h.HotelId,
                HotelName = h.HotelName,
                HotelDescription = h.HotelDescription,
                HotelPriceRange = h.HotelPriceRange,
                HotelLocation = h.HotelLocation,
                LocationId = h.LocationId,
                facilities = facilities
            })
            .FirstOrDefault(h => h.HotelId == id);
        var reviews = findAllReview(id);
        var countReview = reviews.Count();
        var totalStar = getSumOfReviewStars(id);
        double star = 0;
        if (countReview != 0) {
            star = (double)totalStar / (double)countReview;
        }
        return new
        {
            hotel = hotel,
            photos = findAllPhoto(id, 5),
            listReview = reviews,
            count = countReview,
            star = Math.Round(star, 1)
        };
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
        int totalItems = totalItem();
        int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        return totalPages;
    }

    public int totalItem()
    {
        return db.Hotels.Count(b => b.IsHide == false);
    }

    public List<ReviewDto> findAllReview(int hotelId)
    {
        return (from review in db.Reviews
                join user in db.Users on review.UserId equals user.UserId
                where review.HotelId == hotelId && review.IsHide == false
                select new ReviewDto
                {
                    ReviewId = review.ReviewId,
                    ReviewStar = review.ReviewStar,
                    ReviewText = review.ReviewText,
                    UserId = review.UserId,
                    UserFullName = user.Fullname,
                    HotelId = review.HotelId
                }).ToList();
    }

    public int getSumOfReviewStars(int hotelId)
    {
        return findAllReview(hotelId).Sum(r => r.ReviewStar);
    }
}
