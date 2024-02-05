using Mango.Services.EmailsAPI.Data;
using Mango.Services.EmailsAPI.extention_classes;
using Mango.Services.EmailsAPI.Messaging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//adding the EF to the pipeline 
builder.Services.AddDbContext<EmailDBContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
//adding the IServiceBusProcessor to the pipeline
builder.Services.AddSingleton<IServiceBusProcessor, ServiceBusProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//adding the extenstion method tp the pipeLine so that the app can run it with the api startup and close with the app closing
app.ServiceBusExtentionApplicationBuilder();
app.MapControllers();

app.Run();
