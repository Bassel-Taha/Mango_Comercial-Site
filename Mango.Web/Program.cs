using Mango.Web.services.Iservices;
using Mango.Web.services;
using Mango.Web.utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region the survices must be added before the build of the application


//add the httpclient and the httpclientfactory to the services
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

//adding the httpclient for the coupon service
builder.Services.AddHttpClient<ICouponService, CouponService>();

//adding the httpclient for the authservice
builder.Services.AddHttpClient<IAuthService, AuthService>();


//adding the scoped service for the interface and the class for emplemntation and injection in the controlers
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//populate CouponBaseURL in the SD class  with the values from the appsettings.json
SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponService"];
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthService"];

#endregion

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
