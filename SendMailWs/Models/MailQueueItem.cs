using MailLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMailWs.Models
{
    public class MailQueueItem : sppMailItem
    {        
        public string itemId { get; set; }

        public MailQueueItem() : base() 
        {
            itemId = String.Empty;
        }
    }
}
