using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CrossStatusRoleEvent
    {
        public int CaseStatusID { get; set; }
        public string CaseRole { get; set; }
        public string EventID { get; set; }

    }
}
