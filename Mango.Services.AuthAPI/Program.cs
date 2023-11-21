using Mango.Services.AuthAPI.Models;
using Mango.Services.CouponAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adding dbcontext to the services
builder.Services.AddDbContext<AppDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

//adding identityframework to the services and connecting it to the dbcontext usiing the entity framework stores
//and adding the default token providers
builder.Services.AddIdentity<ApplicationUsers, IdentityRole>()
    .AddEntityFrameworkStores<AppDBContext>()
    .AddDefaultTokenProviders();

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
