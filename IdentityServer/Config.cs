using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using System.Security.Claims;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "rdv_mvc_client",
                    ClientName = "Rdv MVC Web App",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:5001/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:5001/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile, "rdvAPI",
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles"
                    }
                }
            };

    public static IEnumerable<ApiScope> ApiScopes =>
       new ApiScope[]
       {
               new ApiScope("rdvAPI", "RendezVous API")
       };

    public static IEnumerable<ApiResource> ApiResources =>
       new ApiResource[]
       {
       };

    public static IEnumerable<IdentityResource> IdentityResources =>
      new IdentityResource[]
      {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource(
                  "roles",
                  "Your role(s)",
                  new List<string>() { "role" })
      };

    public static List<TestUser> TestUsers =>
    new List<TestUser>
    {
            new TestUser
            {
                SubjectId = "70A4DF29-17CB-4250-B480-C1ED75F84CE9",
                Username = "lafouine",
                Password = "lafouine",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.GivenName, "lafouine"),
                    new Claim(JwtClaimTypes.FamilyName, "lechat")
                }
            }
    };
}