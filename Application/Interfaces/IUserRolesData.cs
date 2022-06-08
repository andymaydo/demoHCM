using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRolesData
    {
        Task<int> AddUserInRole(string roleName, string LoginName, int ModifyUserID);
        Task<int> DelUserInRole(string roleName, string LoginName, int ModifyUserID);
        List<UserRolesModel> GetLocalizatedNames();
        bool IsUserInRole(string username, string rolename);
    }
}