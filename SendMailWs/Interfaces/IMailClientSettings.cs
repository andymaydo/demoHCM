using MailLib.Models;
using SendMailWs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMailWs.Interfaces
{
    public interface IMailClientSettings
    {
        Task<MailClientSettings> GetSettings();
    }
}
