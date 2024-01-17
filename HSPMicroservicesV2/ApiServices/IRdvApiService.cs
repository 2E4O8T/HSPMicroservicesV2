using HSPMicroservicesV2.Models;

namespace HSPMicroservicesV2.ApiServices
{
    public interface IRdvApiService
    {
        Task<IEnumerable<Rdv>> GetRdvs();
        Task<Rdv> GetRdv(string id);
        Task<Rdv> CreateRdv(Rdv rdv);
        Task<Rdv> UpdateRdv(Rdv rdv);
        Task DeleteRdv(int id);
        Task<UserInfoViewModel> GetUserInfo();
    }
}
