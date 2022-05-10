using HCMModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMDataAccess
{
    public interface ILoginData
    {
        Task<LoginResponse> Login(string username, string password);
        Task<List<string>> UserRolesGetAsync(string userLogin);
    }
}