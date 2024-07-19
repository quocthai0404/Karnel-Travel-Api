using KarnelTravel.DTO;

namespace KarnelTravel.Services.Tours;

public interface ITourService
{
    public List<TourDTOAndMainPhoto> findAllTourDto();

    public List<TourDTOAndMainPhoto> findAllTour();

    public TourDTOAndMainPhoto findById(int id);
}
