using KarnelTravel.DTO;
using KarnelTravel.Services.Account;
using KarnelTravel.Services.Airport;
using KarnelTravel.Services.Beach;
using KarnelTravel.Services.Bookings.BookingFlight;
using KarnelTravel.Services.Bookings.BookingHotel;
using KarnelTravel.Services.Bookings.BookingTour;
using KarnelTravel.Services.Discounts;
using KarnelTravel.Services.Facilities;
using KarnelTravel.Services.Flights;
using KarnelTravel.Services.Hotels;
using KarnelTravel.Services.Mail;
using KarnelTravel.Services.Photo;
using KarnelTravel.Services.Reviews;
using KarnelTravel.Services.Tours;

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
        services.AddScoped<IHotelService, HotelServiceImpl>();
        services.AddScoped<IFacilityService, FacilityServiceImpl>();
        services.AddScoped<IFlightService, FlightServiceImpl>();
        services.AddScoped<IDiscountService, DiscountServiceImpl>();
        services.AddScoped<IHotelBookingService, HotelBookingServiceImpl>();
        services.AddScoped<IFlightBookingService, FlightBookingServiceImpl>();
        services.AddScoped<IBookingTourService, BookingTourServiceImpl>();
        services.AddScoped<ITourService, TourServiceImpl>();
        services.AddScoped<IReviewService, ReviewServiceImpl>();
    }
}
