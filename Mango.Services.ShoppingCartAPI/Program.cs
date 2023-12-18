
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Mango.Services.ShoppingCartAPI;
using Mango.Services.ShoppingCartAPI.Data;

using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adding the services for the api 
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
