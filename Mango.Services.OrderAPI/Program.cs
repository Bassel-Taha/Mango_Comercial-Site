using Mango.Services.OrderAPI;
using Mango.Services.OrderAPI.Data;
using Mango.Services.OrderAPI.Services;
using Mango.Services.OrderAPI.Services.IServices;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//adding the dbcontext to the pipeline
builder.Services.AddDbContext<OrderDBContext>(
    options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

#region adding automapper config in the pipeLine

builder.Services.AddAutoMapper(typeof(AutomapperConfig));

#endregion
#region adding the http configurations to get the products from the productAPI

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("Products", x => x.BaseAddress = new Uri(builder.Configuration["ProductsBaseUrl"]));
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
