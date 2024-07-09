using KarnelTravel.DTO;
using KarnelTravel.Models;

namespace KarnelTravel.Services.Facilities;

public class FacilityServiceImpl : IFacilityService
{
    private DatabaseContext db;
    public FacilityServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }
    public List<FacilityDTO> findAll(int hotelId)
    {
        var hotel = db.Hotels.SingleOrDefault(h => h.HotelId == hotelId && h.IsHide == false);
        return hotel.Facilities.Select(f => new FacilityDTO {
            FacilityId = f.FacilityId,
            FacilityName = f.FacilityName
        }).ToList();
            
    }

    
}
