using KarnelTravel.DTO;
using KarnelTravel.Models;
using KarnelTravel.Query;

namespace KarnelTravel.Services.Hotels;

public interface IHotelService
{
    public List<HotelAndMainPhoto> listHotelsPaginated(QueryObject query, int pageSize);
    public List<HotelDTO> findDTOsPaginated(QueryObject query, int skipNumber, int pageSize);
    public List<PhotoDTO> findAllPhoto(int hotelId, int n);
    
    public PhotoDTO findMainPhoto(int hotelId);

    public int totalPages(QueryObject query, int pageSize);
    public int totalItem(QueryObject query);
    //hotel details
    public dynamic HotelDetails(int id);
    public string findHotelNameById(int hotelId);

    //review
    public List<ReviewDto> findAllReview(int hotelId);
    public int getSumOfReviewStars(int hotelId);

    //room
    public List<RoomDTO> findAllRoom(int hotelId);

    public RoomDTO findRoom(int roomId);
}
