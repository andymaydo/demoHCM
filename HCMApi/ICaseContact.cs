using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMApi
{
    public interface ICaseContact
    {
        string ContactData { get; set; }
        int ContactID { get; set; }
        string ContactURI { get; set; }
        string Email { get; set; }
        string ForeingID { get; set; }
        string Function { get; set; }
        string Name { get; set; }
        string NickName { get; set; }
        string ProfileRole { get; set; }

        string AsXml();
        Task<int> Create();
        Task<int> Create(string vEmail);

        Task<List<CaseContact>> Contact_GetAll();
    }
}