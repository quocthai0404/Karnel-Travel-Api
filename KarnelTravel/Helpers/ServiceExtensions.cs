using KarnelTravel.DTO;
using KarnelTravel.Services.Account;
using KarnelTravel.Services.Airport;
using KarnelTravel.Services.Beach;
using KarnelTravel.Services.Mail;
using KarnelTravel.Services.Photo;

namespace KarnelTravel.Helpers;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        
        services.AddScoped<IAccountService, AccountServiceImpl>();
        services.AddScoped<IMailService, MailServiceImpl>();
        services.AddScoped<IAirportService, AirportServiceImpl>();
        services.AddScoped<IPhotoService, PhotoServiceImpl>();
        services.AddScoped<IBeachService, BeachServiceImpl>();
        
    }
}
