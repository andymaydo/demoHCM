using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMModels;

namespace HCMDataAccess
{
    public interface IAliasManager
    {
        Task<List<AmAlias>> GetByFilterAsync(AmAliasFilter filter, int userId);
        Task<AmAlias> GetByIdAsync(int aliasId);
        Task<List<AmAliasProtocol>> ProtocolGetWaitingByAliasIdAsync(int aliasId);
        Task<List<AmAliasProtocol>> ProtocolGetByAliasIdAsync(int aliasId);
        Task<List<AmAliasReport>> ReportGetByFilterAsync(AmAliasFilter filter, int userId);

        Task ReActivateAsync(int aliasId, string reason, int callerId);
        Task DeActivateAsync(int aliasId, string reason, int callerId);
        Task DeleteAsync(int aliasId, string reason, int callerId);
        Task AuthorizeAsync(int aliasProtokolId, string reason, int callerId);
        Task DiscardAsync(int aliasProtokolId, string reason, int callerId);
    }
}
