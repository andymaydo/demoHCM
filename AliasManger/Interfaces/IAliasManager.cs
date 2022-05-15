using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AliasManger.Models;

namespace AliasManger.Interfaces
{
    public interface IAliasManager
    {
        Task<List<AmAlias>> GetByFilterAsync(string aliasName, string alisStreet, string profilesCSV,
           int statusId, bool waitAuth, string accId);
        Task<AmAlias> GetByIdAsync(int aliasId);
        Task<List<AmAliasProtocol>> ProtocolGetWaitingByAliasIdAsync(int aliasId);
        Task<List<AmAliasProtocol>> ProtocolGetByAliasIdAsync(int aliasId);
        Task<List<AmAliasReport>> ReportGetByFilterAsync(DateTime von, DateTime bis, bool allTime,
                string aliasName, string alisStreet, string profilesCSV, string accId);

        Task ReActivateAsync(int aliasId, string reason, string callerLogin);
        Task DeActivateAsync(int aliasId, string reason, string callerLogin);
        Task DeleteAsync(int aliasId, string reason, string callerLogin);
        Task AuthorizeAsync(int aliasProtokolId, string reason, string callerLogin);
        Task DiscardAsync(int aliasProtokolId, string reason, string callerLogin);


        Task CreateAsync(string aliasName, string aliasStreet, string description,
                int? vgsProfilId, string sapIp, string sapGw, string sapMandant, string sapBelegNr,
                string hcmUserFullName, string hcmProfilId, string hcmProfilName,
                string caseUrl, string accId);
    }
}
