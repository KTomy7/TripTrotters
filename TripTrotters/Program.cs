using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TripTrotters.DataAccess;
using TripTrotters.Helpers;
using TripTrotters.Models;
using TripTrotters.Services;
using TripTrotters.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IApartmentService, ApartmentService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICloudinaryImageService, CloudinaryImageService>();
builder.Services.AddScoped<IUserPostLikeService, UserPostLikeService>();
builder.Services.AddScoped<IUserCommentLikeService, UserCommentLikeService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.Configure<ApCloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddDbContext<TripTrottersDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TripTrottersDatabaseConnection"));

});
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 4;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<TripTrottersDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Traveller", options => options.RequireRole("Traveller"));
    options.AddPolicy("Owner", options => options.RequireRole("Owner"));
    options.AddPolicy("Admin", options => options.RequireRole("Admin"));
});

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    await Seed.SeedUsersAndRolesAsync(app);
    Seed.SeedData(app);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


