namespace KarnelTravel.DTO;

public class UserDTO
{
    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public bool Gender { get; set; }
}
