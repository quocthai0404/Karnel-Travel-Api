using KarnelTravel.DTO;
using KarnelTravel.Models;
using KarnelTravel.Query;
using KarnelTravel.Services.Facilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Globalization;

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
    public List<HotelAndMainPhoto> listHotelsPaginated(QueryObject query, int pageSize)
    {
        var skipNumber = (query.page - 1) * pageSize;


        var hotelDTOs = findDTOsPaginated(query, skipNumber, pageSize);

        

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

    public List<HotelDTO> findDTOsPaginated(QueryObject query, int skipNumber, int pageSize)
    {
        
        var queryResult = db.Hotels.Where(b => b.IsHide == false);

        if (!string.IsNullOrEmpty(query.hotelName))
        {
            queryResult = queryResult.Where(f => f.HotelName.Contains(query.hotelName));
        }

        return queryResult.Select(b => new HotelDTO
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
        var rooms = findAllRoomWithPhoto(id);
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
            star = Math.Round(star, 1), 
            rooms = rooms
        };
    }

 
    public PhotoDTO findMainPhoto(int hotelId)
    {
        return db.Photos
            .Where(p => p.HotelId == hotelId)
            .Select(p => new PhotoDTO { PhotoUrl = p.PhotoUrl })
            .FirstOrDefault();
    }

    public int totalItem(QueryObject query)
    {
        var queryResult = db.Hotels.Where(b => b.IsHide == false);
        if (!string.IsNullOrEmpty(query.hotelName))
        {
            queryResult = queryResult.Where(f => f.HotelName.Contains(query.hotelName));
        }
        return queryResult.Count();
    }

    public int totalPages(QueryObject query, int pageSize)
    {
        int totalItems = totalItem(query);
        int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        return totalPages;
    }

    

    public List<ReviewDto> findAllReview(int hotelId)
    {
        return (from review in db.Reviews
                join user in db.Users on review.UserId equals user.UserId
                where review.HotelId == hotelId && review.IsHide == false
                orderby review.ReviewId descending
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

    public PhotoDTO findMainRoomPhoto( int roomId)
    {
        return db.Photos
            .Where(p => p.RoomId == roomId)
            .Select(p => new PhotoDTO { PhotoUrl = p.PhotoUrl })
            .FirstOrDefault();
    }

    public List<RoomDTO> findAllRoom(int hotelId)
    {
        var rooms = db.Rooms
            .Where(r => r.HotelId == hotelId && r.IsHide == false)
            .Select(r => new RoomDTO
            {
                RoomId = r.RoomId,
                HotelId = r.HotelId,
                RoomName = r.RoomName,
                RoomDescription = r.RoomDescription,
                RoomPrice = r.RoomPrice,
                NumOfSingleBed = r.NumOfSingleBed,
                NumOfDoubleBed = r.NumOfDoubleBed,
                maxPerInRoom = r.NumOfSingleBed * 1 + r.NumOfDoubleBed * 2,
                IsHide = r.IsHide
            })
            .ToList();

        return rooms;
    }

    public List<RoomDTO> findAllRoomWithPhoto(int hotelId)
    {
        var rooms = findAllRoom(hotelId);

        

        foreach (var room in rooms)
        {
            var mainPhoto = findMainRoomPhoto(room.RoomId);
            if (mainPhoto != null)
            {
                room.photoUrl = mainPhoto.PhotoUrl;
            }
            //room.photoUrl = mainPhoto != null ? mainPhoto.PhotoUrl : "https://res.cloudinary.com/dhee9ysz4/image/upload/v1720448925/dm6mc5s3zagzkl8zrsow.jpg";
        }

        return rooms;
    }

    public RoomDTO findRoom(int roomId)
    {
        return db.Rooms
            .Where(r => r.RoomId == roomId && r.IsHide == false)
            .Select(r => new RoomDTO
            {
                RoomId = r.RoomId,
                HotelId = r.HotelId,
                RoomName = r.RoomName,
                RoomDescription = r.RoomDescription,
                RoomPrice = r.RoomPrice,
                NumOfSingleBed = r.NumOfSingleBed,
                NumOfDoubleBed = r.NumOfDoubleBed,
                maxPerInRoom = r.NumOfSingleBed * 1 + r.NumOfDoubleBed * 2,
                IsHide = r.IsHide
            }).FirstOrDefault();

    }

    public string findHotelNameById(int hotelId)
    {
        return db.Hotels.FirstOrDefault(h => h.HotelId == hotelId).HotelName;
    }
}
