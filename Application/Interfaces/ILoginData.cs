using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILoginData
    {
        Task<LoginResponse> Login(string username, string password);
        Task<List<string>> UserRolesGetAsync(string userLogin);
    }
}