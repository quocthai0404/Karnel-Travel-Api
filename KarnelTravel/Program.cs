using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using KarnelTravel.Helpers;
using KarnelTravel.Models;
using KarnelTravel.Services.Account;
using KarnelTravel.Services.Mail;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
//Cloudinary cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
//cloudinary.Api.Secure = true;
//var uploadParams = new ImageUploadParams()
//{
//    File = new FileDescription(@"https://cloudinary-devs.github.io/cld-docs-assets/assets/images/cld-sample.jpg"),
//    UseFilename = true,
//    UniqueFilename = false,
//    Overwrite = true
//};
//var uploadResult = cloudinary.Upload(uploadParams);
//Console.WriteLine(uploadResult.JsonObj);

//develop
//thai
var connectionString = "Server=DESKTOP-6L06R65;Database=Karnel_Travel;user id=sa;password=123456;trusted_connection=true;encrypt=false";


//product
//var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

builder.Services.AddDbContext<DatabaseContext>(option =>
{
    option.UseLazyLoadingProxies().UseSqlServer(connectionString);
});


// Add services to the container.
//builder.Services.AddScoped<IAccountService, AccountServiceImpl>();
//builder.Services.AddScoped<IMailService, MailServiceImpl>();
builder.Services.AddCustomServices();

//


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();
app.UseAuthentication();

app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
