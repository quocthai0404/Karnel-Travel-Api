using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class RoomHotelQuantity
{
    public int HotelId { get; set; }

    public int RoomId { get; set; }

    public int QuantityMax { get; set; }

    public int QuantityLeft { get; set; }
}
