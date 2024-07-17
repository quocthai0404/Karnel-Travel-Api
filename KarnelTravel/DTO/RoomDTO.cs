namespace KarnelTravel.DTO;

public class RoomDTO
{
    public int RoomId { get; set; }

    public int HotelId { get; set; }

    public string RoomName { get; set; } = null!;

    public string RoomDescription { get; set; } = null!;

    public float RoomPrice { get; set; }

    public int NumOfSingleBed { get; set; }

    public int NumOfDoubleBed { get; set; }

    public int maxPerInRoom { get; set; } = 0;

    public string photoUrl { get; set; } = "https://res.cloudinary.com/dhee9ysz4/image/upload/v1720448925/dm6mc5s3zagzkl8zrsow.jpg";

    public bool IsHide { get; set; }
}
