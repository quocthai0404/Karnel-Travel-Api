using KarnelTravel.DTO;

namespace KarnelTravel.Services.Hotels;

public interface IHotelService
{
    public List<HotelAndMainPhoto> listHotelsPaginated(int pageNumber, int pageSize);
    public List<HotelDTO> findDTOsPaginated(int skipNumber, int pageSize);
    public List<PhotoDTO> findAllPhoto(int hotelId);
    public HotelDTO findByIdDTO(int id);
    public PhotoDTO findMainPhoto(int hotelId);
    public int totalPages(int pageSize);
}
