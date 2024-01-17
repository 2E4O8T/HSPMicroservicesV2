using HSPMicroservicesV2.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;

namespace HSPMicroservicesV2.ApiServices
{
    public class RdvApiService : IRdvApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RdvApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            //_httpClientFactory = httpClientFactory;
            _httpClientFactory = httpClientFactory ?? throw new ArgumentException(nameof(httpClientFactory));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentException(nameof(httpContextAccessor));
        }

        public async Task<UserInfoViewModel> GetUserInfo()
        {
            var idpClient = _httpClientFactory.CreateClient("IDPClient");

            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();
            if (metaDataResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong while requesting the access token");
            }

            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var userInfoResponse = await idpClient.GetUserInfoAsync(
                new UserInfoRequest
                {
                    Address = metaDataResponse.UserInfoEndpoint,
                    Token = accessToken
                });

            if (userInfoResponse.IsError)
            {
                throw new HttpRequestException("Something went wrong while getting user info");
            }

            var userInfoDictionary = new Dictionary<string, string>();

            foreach (var claim in userInfoResponse.Claims)
            {
                userInfoDictionary.Add(claim.Type, claim.Value);
            }

            return new UserInfoViewModel(userInfoDictionary);
        }

        public async Task<IEnumerable<Rdv>> GetRdvs()
        {
            // Way2 - Better Way
            var httpClient = _httpClientFactory.CreateClient("RdvAPIClient");

            // SEction 11
            // var request = new HttpRequestMessage(HttpMethod.Get, "/api/rdvs/");
            var request = new HttpRequestMessage(HttpMethod.Get, "/gateway/rdvs");

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var rdvList = JsonConvert.DeserializeObject<List<Rdv>>(content);
            
            return rdvList;
        }

        public Task<Rdv> GetRdv(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Rdv> CreateRdv(Rdv rdv)
        {
            throw new NotImplementedException();
        }

        public Task<Rdv> UpdateRdv(Rdv rdv)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRdv(int id)
        {
            throw new NotImplementedException();
        }
    }
}
