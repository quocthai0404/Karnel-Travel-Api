using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int HotelId { get; set; }

    public string RoomName { get; set; } = null!;

    public string RoomDescription { get; set; } = null!;

    public float RoomPrice { get; set; }

    public int NumOfSingleBed { get; set; }

    public int NumOfDoubleBed { get; set; }

    public bool IsHide { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual ICollection<HotelInvoice> HotelInvoices { get; set; } = new List<HotelInvoice>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
