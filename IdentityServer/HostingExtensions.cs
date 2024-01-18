using Duende.IdentityServer.Test;
using identityserver;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

namespace IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // uncomment if you want to add a UI
        //builder.Services.AddRazorPages();

        // Ajout modification - Commentaire sur builder.Services.AddControllersWithViews();
        // Ajout modification - Razor Pages
        // builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Pages/Account/Login"; 
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAuthenticatedUser", policy =>
            {
                policy.RequireAuthenticatedUser();
            });
        });
        // Fin Ajout

        builder.Services.AddIdentityServer(options =>
        {
            // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
            options.EmitStaticAudienceClaim = true;
        })
            .AddInMemoryClients(Config.Clients)
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            //.AddTestUsers(Config.TestUsers)
            .AddTestUsers(TestUsers.Users)
            .AddDeveloperSigningCredential();

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // uncomment if you want to add a UI
        app.UseStaticFiles();
        app.UseRouting();
            
        app.UseIdentityServer();

        // uncomment if you want to add a UI
        app.UseAuthorization();

        // Ajout Suppression commentaire
        app.MapRazorPages().RequireAuthorization();

        // Ajout
        //app.UseEndpoints(endpoints =>
        //{
        //    endpoints.MapDefaultControllerRoute();
        //});
        app.UseEndpoints(endpoints =>
        {
            //endpoints.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });


        return app;
    }
}
