using HCMModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMDataAccess
{
    public interface ISettingsData
    {
        Task<int> ChangeSettings(int IssuedByID, string SmtpServer, string SmtpPort, bool isAuth, string SmtpUser, string SmtpPwd);
        Task<SettingsModel> GetSettings();
    }
}