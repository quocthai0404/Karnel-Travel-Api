namespace KarnelTravel.DTO;

public class BeachDTO
{
    public int BeachId { get; set; }

    public string BeachName { get; set; } = null!;

    public string? BeachLocation { get; set; }

    public int? LocationId { get; set; }
}
