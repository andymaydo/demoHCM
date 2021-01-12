using HCMDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMDataAccess
{
    public interface IUserRolesData
    {
        Task<int> AddUserInRole(string roleName, string LoginName, int ModifyUserID);
        Task<int> DelUserInRole(string roleName, string LoginName, int ModifyUserID);
        List<UserRolesModel> GetLocalizatedNames();
    }
}