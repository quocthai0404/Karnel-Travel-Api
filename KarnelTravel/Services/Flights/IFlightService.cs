using KarnelTravel.DTO;
using KarnelTravel.Query;

namespace KarnelTravel.Services.Flights;

public interface IFlightService
{
    public List<FlightDTO> getAllFlight(QueryObject ob);
    public FlightDTO GetFlightDTO(int id);

}
