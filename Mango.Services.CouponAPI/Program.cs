using Mango.Services.AuthAPI.Services;
using Mango.Services.AuthAPI.Services.IServices;
using Mango.Services.CouponAPI;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Modes.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adding automapper to the services
builder.Services.AddAutoMapper(typeof(MapperConfigration));

////adding the IAuthservice and the Authservice to the services
//builder.Services.AddScoped<IAuthService, AuthService>();

//adding dbcontext to the services
builder.Services.AddDbContext<AppDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));



//adding responsDTO to the services for the dependency injection
builder.Services.AddScoped<ResponsDTO>();

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

//using the function for applying pending migration automaticly 
applyMigrations();

app.Run();


//creating function to migrate the bending migrations to the database at the time of startup
void applyMigrations()
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<AppDBContext>();
        if (context.Database.GetPendingMigrations().Count()>0)
        {
            context.Database.Migrate();
        }
    };
    
}
