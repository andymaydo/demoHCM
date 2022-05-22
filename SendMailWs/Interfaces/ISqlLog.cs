using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMailWs.Interfaces
{
    public interface ISqlLog
    {
        Task AddSqlLog(string errCode, string errMsg, Exception ex);
    }
}
