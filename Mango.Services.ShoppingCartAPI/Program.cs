
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Mango.Services.ShoppingCartAPI;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Services;
using Mango.Services.ShoppingCartAPI.Services.IServices;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Mango.Services.ShoppingCartAPI.DelegatingHandler;
using EmailServiceBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#region authentication and authorization to swagger
builder.Services.AddSwaggerGen(c =>
    {
        //c.CustomSchemaIds(type => type.FullName);
        c.SwaggerDoc("v1", new() { Title = "Mango.Services.ShoppingCart", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                                              {
                                                  Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                                                  Name = "Authorization",
                                                  In = ParameterLocation.Header,
                                                  Type = SecuritySchemeType.ApiKey,
                                                  Scheme = "Bearer"
                                              });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                                     {
                                         {
                                             new OpenApiSecurityScheme
                                                 {
                                                     Reference = new OpenApiReference
                                                                     {
                                                                         Type = ReferenceType.SecurityScheme,
                                                                         Id = "Bearer"
                                                                     },
                                                     Scheme = "oauth2",
                                                     Name = "Bearer",
                                                     In = ParameterLocation.Header,

                                                 },
                                             new List<string>()
                                         }
                                     });
    });
#endregion

//adding the services for the api 

#region adding the http client for the products api
/// gonna use delegattingHandeler to pass the token to the coupons api or any other api 

// adding the HttpContextaccessor to get the token from the cookie
builder.Services.AddHttpContextAccessor();

//has to add the coupondelegattinghandeller as a transient to the pipeline
builder.Services.AddTransient<CouponsDelegattingHandler>();

SD.CouponApi = builder.Configuration["CouponsBaseUrl"];

builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICouponService, CouponServices>();
builder.Services.AddHttpClient("Products", x => x.BaseAddress = new Uri(builder.Configuration["ProductsBaseUrl"]));

// has to add the httpmessagehandler to the httpclient to pass the token to the api with the class of the delegatting handler
builder.Services.AddHttpClient("Coupons", x => x.BaseAddress = new Uri(builder.Configuration["CouponsBaseUrl"]))
    .AddHttpMessageHandler<CouponsDelegattingHandler>();


#endregion

#region adding the DB context service
builder.Services.AddDbContext<ShoppinCartDB_Context>(options =>
       options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
#endregion

#region automapper service
builder.Services.AddAutoMapper(typeof(AutomapperConfigrations));
#endregion

#region the 3 thing we validate the token with
// the key was encoded using the ascii encoding
var key = Encoding.ASCII.GetBytes(builder.Configuration["ApiSettings:secret"]);
var issuer = builder.Configuration["ApiSettings:issuer"];
var audience = builder.Configuration["ApiSettings:Audience"];
#endregion

#region adding the JWT authentication and authorization service

builder.Services.AddAuthentication(
    (x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })).AddJwtBearer(
    options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
                                                    {
                                                        ValidAudience = audience,
                                                        ValidateAudience = true,
                                                        ValidIssuer = issuer,
                                                        ValidateIssuer = true,
                                                        ValidateIssuerSigningKey = true,
                                                        IssuerSigningKey = new SymmetricSecurityKey(key),
                                                        RequireExpirationTime = true,
                                                        ValidateLifetime = true,
                                                        ClockSkew = TimeSpan.Zero
                                                    };
        });

builder.Services.AddAuthorization();

#endregion

#region to send the the cart via email using the emailSurviceBus we have to add it to the pipeline and to show here we have to add the message bus project as a depenendency in the cart api project

builder.Services.AddScoped<IMessageServiceBus, MessageServiceBus >();

#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
