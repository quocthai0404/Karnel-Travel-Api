using KarnelTravel.DTO;
using KarnelTravel.Models;

namespace KarnelTravel.Services.Hotels;

public interface IHotelService
{
    public List<HotelAndMainPhoto> listHotelsPaginated(int pageNumber, int pageSize);
    public List<HotelDTO> findDTOsPaginated(int skipNumber, int pageSize);
    public List<PhotoDTO> findAllPhoto(int hotelId, int n);
    
    public PhotoDTO findMainPhoto(int hotelId);
    public int totalPages(int pageSize);
    public int totalItem();
    //hotel details
    public dynamic HotelDetails(int id);

    //review
    public List<ReviewDto> findAllReview(int hotelId);
    public int getSumOfReviewStars(int hotelId);
}
