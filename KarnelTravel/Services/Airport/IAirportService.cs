using KarnelTravel.DTO;
using KarnelTravel.Models;

namespace KarnelTravel.Services.Airport;

public interface IAirportService
{
    // crud 
    public bool create(KarnelTravel.Models.Airport airport);
    public bool update(KarnelTravel.Models.Airport airport);
    public bool delete(string airportId);
    public KarnelTravel.Models.Airport findById(string airportId);
    public AirportDTO findByIdDTO(string airportId);
    public List<AirportDTO> findAll();
    public List<AirportDTO> findAllDeleted();
    public bool Recover(string airportId);
}
