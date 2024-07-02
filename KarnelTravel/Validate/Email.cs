using System.Net.Mail;

namespace KarnelTravel.Validate;

public class Email
{
    public static bool IsValidEmailAddress(string emailAddress)
    {
        try
        {
            new MailAddress(emailAddress);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
