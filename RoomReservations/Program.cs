using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reCAPTCHA.AspNetCore;
using RoomReservationsBLL.Services;
using RoomReservationsBLL.Validators.Booking;
using RoomReservationsDAL.Reservations;
using RoomReservationsDAL.Reservations.Repositories;
using RoomReservationsVM.Configuration;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Add app and secret settings/values
builder.Services.AddDbContext<ReservationsDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:ReservationsConnection"]));
builder.Services.Configure<AppValues>(builder.Configuration.GetSection("AppValues"));
builder.Services.Configure<MailCredentials>(builder.Configuration.GetSection("MailCredentials"));

// Add services
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IRegistryService, RegistryService>();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBookingValidator, BookingValidator>();
builder.Services.AddScoped<IMailingService, MailingService>();

builder.Services.AddMvc(options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); });

// Add reCAPTCHA
builder.Services.AddRecaptcha(options =>
{
    options.SiteKey = builder.Configuration["AppValues:ReCaptchaSiteKey"];
    options.SecretKey = builder.Configuration["ReCaptchaSettings:SecretKey"];
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error/GenericError");
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/Error/StautsCodeError?statusCode={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RoomView}/{action=Index}/{id?}");

app.Run();