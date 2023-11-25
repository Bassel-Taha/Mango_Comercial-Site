using Mango.Services.AuthAPI;
using Mango.Services.AuthAPI.Models.DTOs;
using Mango.Services.AuthAPI.Models.DTOs.UserDtos;
using Mango.Services.AuthAPI.Services;
using Mango.Services.AuthAPI.Services.IServices;
using Mango.Services.CouponAPI.Data;
using Mango.Web.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResponsDTO = Mango.Services.AuthAPI.Models.DTOs.ResponsDTO;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddControllers();

//adding dbcontext to the services
builder.Services.AddDbContext<AppDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

//adding identityframework to the services and connecting it to the dbcontext usiing the entity framework stores
///and adding the default token providers
builder.Services.AddIdentity<ApplicationUsers, IdentityRole>()
    .AddEntityFrameworkStores<AppDBContext>()
    .AddDefaultTokenProviders();


//adding responseDTO to the services forn injection
builder.Services.AddScoped<ResponsDTO>();

//adding the JwtTokenGenerator and the IJwtTokenGenerator to the services of the project
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

//adding the Authservice and the IAuthservice to the services of the project 
builder.Services.AddScoped<IAuthService, AuthService>();

//configring the JWTConfig class so then we can use it in the controllers by injecting it
builder.Services.Configure<JWTConfigration>(builder.Configuration.GetSection("ApiSettings:JWTOptions"));

//ading automapper to the services
builder.Services.AddAutoMapper(typeof(MapperConfigration));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//adding authentication and authorization to the ussage of the API
///the authentication Must be before the authorization for the app to run correctly 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//using the function for applying pending migration automaticly 
applyMigrations();

app.Run();


//creating function to migrate the bending migrations to the database at the time of startup
void applyMigrations()
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<AppDBContext>();
        if (context.Database.GetPendingMigrations().Count() > 0)
        {
            context.Database.Migrate();
        }
    };

}
