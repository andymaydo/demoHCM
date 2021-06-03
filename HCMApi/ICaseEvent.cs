using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMApi
{
    public interface ICaseEvent
    {
        Task<List<CaseEvent>> GetHistoryByCase(int CaseID);
        Task<int> Create();
        IConfiguration GetIConfiguration();
    }
}
