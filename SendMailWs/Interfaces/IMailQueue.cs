using MailLib.Models;
using SendMailWs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMailWs.Interfaces
{
    public interface IMailQueue
    {
        Task<List<MailQueueItem>> GetTop(int top = 20);
        Task SetStatus(string recId, string statusId);        

    }
}
