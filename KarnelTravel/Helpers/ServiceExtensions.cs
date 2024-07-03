using KarnelTravel.Services.Account;
using KarnelTravel.Services.Airport;
using KarnelTravel.Services.Mail;

namespace KarnelTravel.Helpers;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountServiceImpl>();
        services.AddScoped<IMailService, MailServiceImpl>();
        services.AddScoped<IAirportService, AirportServiceImpl>();
    }
}
