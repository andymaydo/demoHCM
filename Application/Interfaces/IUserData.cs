using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{

    public interface IUsersData
    {
        Task<int> ChangePassword(int UserID, string newPass, int ModifyUserID);
        Task<int> CreateUser(string LoginName, string Password, string Email, string FullName, string Department, bool Enable, int ModifyUserID);
        Task<int> DeleteUser(int UserID, int ModifyUserID);
        Task<int> SaveData(int userID, string Email, string FullName, string Department, bool Enable, int ModifyUserID);
        Task<List<UsersModel>> UsersList(int userID);
    }
}
