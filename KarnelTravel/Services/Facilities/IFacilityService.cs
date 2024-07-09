using KarnelTravel.DTO;

namespace KarnelTravel.Services.Facilities;

public interface IFacilityService
{
    public List<FacilityDTO> findAll(int hotelId);
}
