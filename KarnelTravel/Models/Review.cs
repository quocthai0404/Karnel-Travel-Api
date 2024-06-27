using System;
using System.Collections.Generic;

namespace KarnelTravel.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public byte ReviewStar { get; set; }

    public string ReviewText { get; set; } = null!;

    public int UserId { get; set; }

    public int HotelId { get; set; }

    public int RestaurantId { get; set; }

    public bool IsHide { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
