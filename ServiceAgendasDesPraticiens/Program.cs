using Microsoft.EntityFrameworkCore;
using ServiceAgendasDesPraticiens.Data;

var builder = WebApplication.CreateBuilder(args);

// Base de donn�es
var connectionString = builder.Configuration.GetConnectionString("CalendarConnection");
builder.Services.AddDbContext<HSPCalendarDbContext>(options =>
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
