using Mango.Services.OrderAPI;
using Mango.Services.OrderAPI.Data;
using Mango.Services.OrderAPI.Services;
using Mango.Services.OrderAPI.Services.IServices;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#region authentication and authorization to swagger
builder.Services.AddSwaggerGen(c =>
{
    //c.CustomSchemaIds(type => type.FullName);
    c.SwaggerDoc("v1", new() { Title = "Mango.Services.ShoppingOrderAPI", Version = "v1" });
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
#endregion//adding the dbcontext to the pipeline

#region the configration for the db context and the connection string

builder.Services.AddDbContext<OrderDBContext>(
    options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

#endregion

#region adding automapper config in the pipeLine

builder.Services.AddAutoMapper(typeof(AutomapperConfig));

#endregion

#region adding the http configurations to get the products from the productAPI

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("Products", x => x.BaseAddress = new Uri(builder.Configuration["ProductsBaseUrl"]));
#endregion

#region the 3 thing we validate the token with
// the key was encoded using the ascii encoding
var key = Encoding.ASCII.GetBytes(builder.Configuration["ApiSettings:secret"]);
var issuer = builder.Configuration["ApiSettings:issuer"];
var audience = builder.Configuration["ApiSettings:Audience"];
#endregion

#region adding the JWT authentication and authorization service

builder.Services.AddAuthentication(
    x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(
    options => options.TokenValidationParameters = new TokenValidationParameters()
                                                       {
                                                           ValidAudience = audience,
                                                           ValidateAudience = true,
                                                           ValidIssuer = issuer,
                                                           ValidateIssuer = true,
                                                           IssuerSigningKey = new SymmetricSecurityKey(key),
                                                           ValidateIssuerSigningKey = true,
                                                           RequireExpirationTime = true,
                                                           ValidateLifetime = true,
                                                           ClockSkew = TimeSpan.Zero
                                                       });


builder.Services.AddAuthorization();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
