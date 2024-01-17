using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServicePriseDeRendezvous.Data;

var builder = WebApplication.CreateBuilder(args);

// Bases de données
var connectionString = builder.Configuration.GetConnectionString("AppointmentConnection");
builder.Services.AddDbContext<HSPAppointmentDbContext>(options =>
options.UseSqlServer(connectionString));

var connectionRdvString = builder.Configuration.GetConnectionString("RdvConnection");
builder.Services.AddDbContext<RdvsApiContext>(options =>
options.UseSqlServer(connectionRdvString));


// Bearer Token
builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5005";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "rdvClient", "rdv_mvc_client"));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rdvs.Api v1"));

    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
//app.MapControllers();

app.Run();
