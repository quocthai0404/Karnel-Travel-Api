namespace KarnelTravel.DTO;

public class BookingHotel
{
    public int hotelid {  get; set; }
    public int roomid { get; set; }
    public double roomprice { get; set; }
    public string checkin { get; set; }
    public string checkout { get; set; }
    public int numadult { get; set; }
    public int numchild { get; set; }
    public int numday { get; set; }
    public double subtotal { get; set; }
    public double tax { get; set; }
    public string code { get; set; }
    public double percent { get; set; }
    public double total { get; set; }
}
