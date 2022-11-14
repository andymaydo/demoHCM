using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFiltersData
    {
        List<FiltersModels.CategoryModel> GetCategories(int appID);
        
        List<FiltersModels.GateModel> GetGates();
        List<FiltersModels.ProfileModel> GetProfiles(int? contactID);
        List<FiltersModels.CaseResultModel> GetResults(int appID);
        List<FiltersModels.CaseStatusModel> GetStatuses();

        List<FiltersModels.CaseStatusModel> GetStatuses4Application(int appID);        
        List<FiltersModels.CaseStatusModel> GetStatusesForEscalation();
    }
}