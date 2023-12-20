using Microsoft.EntityFrameworkCore;
using ServicePriseDeRendezvous.Data;

var builder = WebApplication.CreateBuilder(args);

// Base de données
var connectionString = builder.Configuration.GetConnectionString("AppointmentConnection");
builder.Services.AddDbContext<HSPAppointmentDbContext>(options =>
options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
