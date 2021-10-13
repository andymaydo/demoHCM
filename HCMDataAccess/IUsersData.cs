using HCMModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMDataAccess
{
    public interface IUsersData
    {
        Task<int> ChangePassword(int UserID, string newPass, int ModifyUserID);
        Task<int> CreateUser(string LoginName, string Password, string Email, string FullName, string Department, bool Enable, int ModifyUserID);
        Task<int> DeleteUser(int UserID, int ModifyUserID);
        Task<int> IsUsernameExists(string LoginName);
        Task<int> SaveData(int userID, string Email, string FullName, string Department, bool Enable, int ModifyUserID);
        Task<List<UsersModel>> UsersList(int userID);
    }
}