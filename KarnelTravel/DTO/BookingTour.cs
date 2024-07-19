namespace KarnelTravel.DTO;

public class BookingTour
{
    public int tourid { get; set; }

    public double tourprice { get; set; }

    public int numofpeo { get; set; }

    public double subtotal { get; set; }
    public double tax { get; set; }
    public string code { get; set; }
    public double percent { get; set; }
    public double total { get; set; }
}
