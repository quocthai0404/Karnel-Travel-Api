namespace KarnelTravel.Helpers;

public class RandomHelper
{
    public static string generateSecurityCode()
    {
        return Guid.NewGuid().ToString().Replace("-", "")+ Guid.NewGuid().ToString().Replace("-", "");
    }

    public static string generateFileName(string fileName)
    {
        var name = Guid.NewGuid().ToString().Replace("-", "");
        var lastIndex = fileName.LastIndexOf('.');
        var extension = fileName.Substring(lastIndex);
        return name + extension;
    }
}
