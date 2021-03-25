using HCMApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMApi
{
    public interface ICMSProfile
    {      

        Task<List<ProfileListModel>> GetList(int? AppID, int? StatusID);
        Task<CMSProfileModel> CreateNew(int ModifiedBy, int appID, string profileName, string profilDescr, string EmailLanguage);
        CMSProfileModel Load(int _profileID);
        Task<bool> UpdateParticipantsAsync(CMSProfileModel cmsProfileModel);
        Task<bool> UpdateGeneralInfoAsync(CMSProfileModel cmsProfileModel);
    }
}