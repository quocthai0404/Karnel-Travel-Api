using KarnelTravel.DTO;
using KarnelTravel.Models;
using KarnelTravel.Services.Facilities;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KarnelTravel.Services.Tours;

public class TourServiceImpl : ITourService
{
    private DatabaseContext db;
    public TourServiceImpl(DatabaseContext _db)
    {
        db = _db;

    }
    public List<TourDTOAndMainPhoto> findAllTour()
    {
        var tours = db.Tours.Where(t => t.IsHide == false);

        var TourDTOs = findAllTourDto();
        var list = TourDTOs.Select(tour =>
        {
            var record = new TourDTOAndMainPhoto()
            {
                TourId = tour.TourId,
                TourName = tour.TourName,
                TourDescription = tour.TourDescription,
                Departure = tour.Departure,
                Arrival = tour.Arrival,
                DepartureProvince = findProvinceName(tour.Departure),
                ArrivalProvince = findProvinceName(tour.Arrival),
                TourPrice = tour.TourPrice,
                IsHide = tour.IsHide,
            };

            var mainPhoto = findMainPhoto(tour.TourId);
            if (mainPhoto != null)
            {
                record.PhotoUrl = mainPhoto.PhotoUrl;
            }
            return record;
        }).ToList();
        return list;

    }

    public List<TourDTOAndMainPhoto> findAllTourDto()
    {
        return db.Tours.Where(t => t.IsHide == false)
        .Select(t => new TourDTOAndMainPhoto {
            TourId = t.TourId,
            TourName = t.TourName,
            TourDescription = t.TourDescription,
            Departure = t.Departure,
            Arrival = t.Arrival,
            TourPrice = t.TourPrice,
            IsHide = t.IsHide,
        }).ToList();
    }

    public TourDTOAndMainPhoto findById(int id)
    {
        var tour = db.Tours.FirstOrDefault(t => t.TourId == id && t.IsHide == false);
        var detail = new TourDTOAndMainPhoto {
            TourId = tour.TourId,
            TourName = tour.TourName,
            TourDescription = tour.TourDescription,
            Departure = tour.Departure, 
            Arrival = tour.Arrival,
            DepartureProvince = findProvinceName(tour.Departure),
            ArrivalProvince = findProvinceName(tour.Arrival),
            TourPrice = tour.TourPrice,
            IsHide = tour.IsHide,
        };
        var mainPhoto = findMainPhoto(id);
        if (mainPhoto != null)
        {
            detail.PhotoUrl = mainPhoto.PhotoUrl;
        }
        return detail;
    }

    public PhotoDTO findMainPhoto(int tourId)
    {
        return db.Photos
            .Where(p => p.TourId == tourId)
            .Select(p => new PhotoDTO { PhotoUrl = p.PhotoUrl })
            .FirstOrDefault();
    }

    public string findProvinceName(int provinceId){
        return db.Provinces.SingleOrDefault(p => p.ProvinceId == provinceId).ProvinceName;
    } 
}
