using Microsoft.EntityFrameworkCore;
using RoomReservationsDAL.Reservations;

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