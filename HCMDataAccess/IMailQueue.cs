using HCMModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMDataAccess
{
    public interface IMailQueue
    {
        Task<List<MailQueueModel>> GetTop(int top = 20);
        Task SetStatus(int recId, int statusId);

     }
}
