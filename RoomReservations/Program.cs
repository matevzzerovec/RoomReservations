using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reCAPTCHA.AspNetCore;
using RoomReservationsBLL.Services.Implementation;
using RoomReservationsBLL.Services.Interface;
using RoomReservationsBLL.Validators.Booking;
using RoomReservationsBLL.Validators.Room;
using RoomReservationsDAL.Reservations;
using RoomReservationsDAL.Reservations.Repositories;
using RoomReservationsVM.Configuration;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Add app and secret settings/values
builder.Services.AddDbContext<ReservationsDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:ReservationsConnection"]));
builder.Services.Configure<AppValues>(builder.Configuration.GetSection("AppValues"));
builder.Services.Configure<MailCredentials>(builder.Configuration.GetSection("MailCredentials"));

// Add services
builder.Services.AddScoped<IRoomNavigationService, RoomNavigationService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IRoomPictureRepository, RoomPictureRepository>();
builder.Services.AddScoped<IRegistryService, RegistryService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBookingValidator, BookingValidator>();
builder.Services.AddScoped<IPictureUploadValidator, PictureUploadValidator>();
builder.Services.AddScoped<IMailingService, MailingService>();

// Authentication
builder.Services
    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ReservationsDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Error/AccessDenied";
});

// Add Antiforgery attribute to all controller actions
builder.Services.AddMvc(options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); });

// Raise upload limit to 256 MB
builder.Services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 268435456; });

// Add reCAPTCHA
builder.Services.AddRecaptcha(options =>
{
    options.SiteKey = builder.Configuration["AppValues:ReCaptchaSiteKey"];
    options.SecretKey = builder.Configuration["ReCaptchaSettings:SecretKey"];
});

// Build app
var app = builder.Build();

// Error/exception handling
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

// Identity roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var adminUsername = builder.Configuration["AdminCredentials:Username"];
    var adminPassword = builder.Configuration["AdminCredentials:Password"];

    await CreateRolesAndAdminUser(roleManager, userManager, adminUsername, adminPassword);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RoomView}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();


async Task CreateRolesAndAdminUser(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, string adminUsername, string adminPassword)
{
    var roleName = "Admin";
    if (!await roleManager.RoleExistsAsync(roleName))
    {
        await roleManager.CreateAsync(new IdentityRole(roleName));
    }

    var adminUser = new IdentityUser
    {
        UserName = adminUsername,
        Email = adminUsername,
        EmailConfirmed = true
    };

    if (await userManager.FindByEmailAsync(adminUser.Email) == null)
    {
        await userManager.CreateAsync(adminUser, adminPassword);
        await userManager.AddToRoleAsync(adminUser, roleName);
    }
}