namespace KarnelTravel.DTO;

public class BookingFlight
{
    public int flightid { get; set; }
    
    public double flightprice { get; set; }
    
    public int numofpass { get; set; }

    public double subtotal { get; set; }
    public double tax { get; set; }
    public string code { get; set; }
    public double percent { get; set; }
    public double total { get; set; }
}
