namespace KarnelTravel.DTO;

public class TourDTOAndMainPhoto
{
    public int TourId { get; set; }

    public string TourName { get; set; } = null!;

    public string TourDescription { get; set; } = null!;

    public int Departure { get; set; }

    public int Arrival { get; set; }

    public string DepartureProvince { get; set; }

    public string ArrivalProvince { get; set; }

    public float TourPrice { get; set; }

    public bool IsHide { get; set; }

    public string PhotoUrl { get; set; } = "https://res.cloudinary.com/dhee9ysz4/image/upload/v1720448925/dm6mc5s3zagzkl8zrsow.jpg";
}
