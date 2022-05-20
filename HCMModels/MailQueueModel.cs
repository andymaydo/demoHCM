using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMModels
{
    public class MailQueueModel
    {
        public int recID { get; set; }
        public string FormAddr { get; set; }
        public string ToAddr { get; set; }
        public string Bcc { get; set; }
        public string Subj { get; set; }
        public string Body { get; set; }
        public string CaseEventData { get; set; }  //xml

    }
}
