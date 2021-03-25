using HCMDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMDataAccess
{
    public interface IFiltersData
    {
        List<FiltersModels.CategoryModel> GetCategories(int appID);
        List<FiltersModels.GateModel> GetGates();
        List<FiltersModels.ProfileModel> GetProfiles(int appID);
        List<FiltersModels.CaseStatusModel> GetStatuses();
        List<FiltersModels.ProfileStatusModel> GetProfileStatuses();
        List<FiltersModels.CaseStatusModel> GetStatusesForEscalation();
    }
}