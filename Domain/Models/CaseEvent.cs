using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CaseEvent
    {
        public int CaseEventID { get; set; }
        public int caseID { get; set; }
        public string eventID { get; set; }
        public string eventName { get; set; }
        public int IssuedByID { get; set; }
        public string ContactName { get; set; }
        public DateTime CreateDate { get; set; }
        public string CaseEventData { get; set; }

        public string CaseEventNotifyContacts { get; set; }
        public string CaseEventText { get; set; }
        public Boolean AddReport { get; set; }
    }
}
