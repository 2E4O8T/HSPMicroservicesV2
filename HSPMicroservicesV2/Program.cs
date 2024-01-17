using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using HSPMicroservicesV2.ApiServices;
using Microsoft.Net.Http.Headers;
using HSPMicroservicesV2.HttpHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Ajout Authentication
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRdvApiService, RdvApiService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Authority = "https://localhost:5010";

    options.ClientId = "rdv_mvc_client";
    options.ClientSecret = "secret";

    options.ResponseType = "code id_token";

    options.Scope.Add("address");
    options.Scope.Add("email");

    options.Scope.Add("rdvAPI");

    options.Scope.Add("roles");

    options.ClaimActions.MapUniqueJsonKey("role", "role");

    options.SaveTokens = true;

    options.GetClaimsFromUserInfoEndpoint = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = JwtClaimTypes.GivenName,
        RoleClaimType = JwtClaimTypes.Role
    };
});

// Ajout HttpClient
// 1. Create an HttpClient used for accessing the Movies API
builder.Services.AddTransient<AuthenticationDelegatingHandler>();

builder.Services.AddHttpClient("RdvAPIClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5005");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

// 2. Create an HttpClient used for accessing the IDP
builder.Services.AddHttpClient("IDPClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5010");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
