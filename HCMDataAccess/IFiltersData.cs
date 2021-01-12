using HCMDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMDataAccess
{
    public interface IFiltersData
    {
        List<FiltersModels.FilterCategoryModel> GetCategories(int appID);
        List<FiltersModels.FilterGateModel> GetGates();
        List<FiltersModels.FilterProfileModel> GetProfiles(int appID);
        List<FiltersModels.FilterStatusModel> GetStatuses();
    }
}