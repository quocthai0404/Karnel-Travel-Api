namespace KarnelTravel.Helpers;

public class RandomHelper
{
    public static string generateSecurityCode()
    {
        return Guid.NewGuid().ToString().Replace("-", "")+ Guid.NewGuid().ToString().Replace("-", "");
    }
}
