using Microsoft.EntityFrameworkCore;
using RoomReservationsBLL.Services;
using RoomReservationsBLL.Validators.Booking;
using RoomReservationsDAL.Reservations;
using RoomReservationsDAL.Reservations.Repositories;
using RoomReservationsVM.Configuration;
using System.Configuration;

// DEV
var builder = WebApplication.CreateBuilder(args);

//PROD
//var builder = WebApplication.CreateBuilder(new WebApplicationOptions
//{
//   EnvironmentName = "Production"
//});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ReservationsDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:ReservationsConnection"]));
builder.Services.Configure<AppValues>(builder.Configuration.GetSection("AppValues"));
builder.Services.Configure<MailCredentials>(builder.Configuration.GetSection("MailCredentials"));

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IRegistryService, RegistryService>();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBookingValidator, BookingValidator>();
builder.Services.AddScoped<IMailingService, MailingService>();

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