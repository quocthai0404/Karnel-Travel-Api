namespace KarnelTravel.Query;

public class QueryObject
{
    public int page { get; set; } = 1;
    public string from { get; set; } = string.Empty;
    public string to { get; set; } = string.Empty;
    public string date { get; set; } = string.Empty;
    public string price { get; set; } = string.Empty;
    public string hotelName { get; set; } = string.Empty;


}
