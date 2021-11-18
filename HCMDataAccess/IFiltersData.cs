using HCMModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMDataAccess
{
    public interface IFiltersData
    {
        List<FiltersModels.CategoryModel> GetCategories(int appID);

        Task<List<FiltersModels.GateModel>> GetGates();
        //List<FiltersModels.GateModel> GetGates();
        List<FiltersModels.ProfileModel> GetProfiles(int appID);
        List<FiltersModels.CaseResultModel> GetResults(int appID);
        List<FiltersModels.CaseStatusModel> GetStatuses();

        List<FiltersModels.CaseStatusModel> GetStatuses4Application(int appID);
        List<FiltersModels.ProfileStatusModel> GetProfileStatuses();
        List<FiltersModels.CaseStatusModel> GetStatusesForEscalation();
    }
}