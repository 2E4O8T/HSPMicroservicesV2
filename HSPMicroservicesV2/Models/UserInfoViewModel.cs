namespace HSPMicroservicesV2.Models
{
    public class UserInfoViewModel
    {
        public Dictionary<string, string> UserInfoDictionary { get; set; } = null;

        public UserInfoViewModel(Dictionary<string, string> userInfoDictionary)
        {
            UserInfoDictionary = userInfoDictionary;
        }
    }
}
