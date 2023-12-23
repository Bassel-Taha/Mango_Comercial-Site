using Mango.Web.services.Iservices;
using Mango.Web.services;
using Mango.Web.utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

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

//adding the authentication for the cookie and the default scheme options
builder.Services.AddAuthentication(options =>
                                            {
                                                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                                                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                                                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                                            }
                                            //adding the cookie options
                                    ).AddCookie(options =>
                                                          {
                                                              options.Cookie.HttpOnly = true;
                                                              options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                                                              options.LoginPath = "/Auth/Login";
                                                              options.AccessDeniedPath = "";
                                                              options.SlidingExpiration = true;
                                                          }
                                    );

//adding the scoped service for the interface and the class for emplemntation and injection in the controlers
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITockenProvider, TockenProvider>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IShoppingCartServicce, ShoppingCartService>();


//populate CouponBaseURL in the SD class  with the values from the appsettings.json
SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponService"];
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthService"];
SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductsService"];
SD.ShoppingCartBase = builder.Configuration["ServiceUrls:ShoppingCartService"];

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
