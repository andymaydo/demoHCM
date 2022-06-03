using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICMSProfile
    {      

        Task<List<CMSProfileModel>> GetList(int? AppID, int? StatusID);
        Task<List<CMSProfileModel>> GetDeleteList(int? AppID);
        Task<CMSProfileModel> CreateNew(int ModifiedBy, int appID, string profileName, string profilDescr, string EmailLanguage);
        Task<CMSProfileModel> ProfilCopy(int SourceProfilID, int ModifiedBy);
        CMSProfileModel Load(int _profileID);
        Task<bool> UpdateParticipantsAsync(CMSProfileModel cmsProfileModel);
        Task<bool> UpdateGeneralInfoAsync(CMSProfileModel cmsProfileModel);
        Task<bool> UpdateEscalationAsync(CMSProfileModel cmsProfileModel);
        Task<bool> UpdateStatusAsync(CMSProfileModel cmsProfileModel);
    }
}