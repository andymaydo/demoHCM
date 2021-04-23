using HCMApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMApi
{
    public interface ICMSProfile
    {      

        Task<List<CMSProfile>> GetList(int? AppID, int? StatusID);
        Task<CMSProfileModel> CreateNew(int ModifiedBy, int appID, string profileName, string profilDescr, string EmailLanguage);
        Task<CMSProfileModel> ProfilCopy(int SourceProfilID, int ModifiedBy);
        CMSProfileModel Load(int _profileID);
        Task<bool> UpdateParticipantsAsync(CMSProfileModel cmsProfileModel);
        Task<bool> UpdateGeneralInfoAsync(CMSProfileModel cmsProfileModel);
        Task<bool> UpdateEscalationAsync(CMSProfileModel cmsProfileModel);
        Task<bool> UpdateStatusAsync(CMSProfileModel cmsProfileModel);
    }
}