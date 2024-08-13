using Microsoft.EntityFrameworkCore;
using RoomReservationsBLL.Services;
using RoomReservationsDAL.Reservations;
using RoomReservationsDAL.Reservations.Repositories;

// DEV
var builder = WebApplication.CreateBuilder(args);

// PROD
//var builder = WebApplication.CreateBuilder(new WebApplicationOptions
//{
//    EnvironmentName = "Production"
//});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ReservationsDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ReservationsConnection")));
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();

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