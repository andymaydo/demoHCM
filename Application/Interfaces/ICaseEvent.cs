using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICaseEvent
    {
        Task<List<CaseEvent>> GetHistoryByCase(int CaseID);
        Task<int> Create(CaseEvent caseEv);
        Task SendMailToNotifyList(int caseEventID, string contacts);
    }
}
