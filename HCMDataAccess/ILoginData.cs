using HCMModels;
using System.Threading.Tasks;

namespace HCMDataAccess
{
    public interface ILoginData
    {
        Task<LoginResponse> Login(string username, string password);
    }
}