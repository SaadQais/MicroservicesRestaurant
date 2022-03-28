using MangoRestaurant.WebMVC;
using MangoRestaurant.WebMVC.Services;
using MangoRestaurant.WebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddHttpClient<ICartService, CartService>();
builder.Services.AddHttpClient<ICouponService, CouponService>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICouponService, CouponService>();

SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
SD.ShoppingCartAPIBase = builder.Configuration["ServiceUrls:ShoppingCartAPI"];
SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies", c =>
    {
        c.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        c.AccessDeniedPath = "/Home/AccessDenied";
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = builder.Configuration["ServiceUrls:IdentityAPI"];
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "mango";
        options.ClientSecret = "secret";
        options.ResponseType = "code";

        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";

        options.ClaimActions.MapJsonKey("role", "role", "role");
        options.ClaimActions.MapJsonKey("sub", "sub", "sub");

        options.Scope.Add("mango");
        options.SaveTokens = true;
        options.RequireHttpsMetadata = false;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
