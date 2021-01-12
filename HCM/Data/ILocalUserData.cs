using System.Threading.Tasks;

namespace HCM.Data
{
    public interface ILocalUserData
    {
        Task<int> GetUserID();
        Task<int> GetContactID();
    }
}