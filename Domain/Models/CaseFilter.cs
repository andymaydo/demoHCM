using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CaseFilterBase
    {
        public DateTime? CreateFromDate { get; set; }
        public DateTime? CreateToDate { get; set; }
        public DateTime? ModifyFromDate { get; set; }
        public DateTime? ModifyToDate { get; set; }

        public int? CaseId { get; set; }
        public string? SearchedName { get; set; }

        public int? MatchTranId { get; set; }
        public string? AddrSourceId { get; set; }

        public int GateId { get; set; } = 1;

        public int? ProfilId { get; set; }
        public int? CategoryId { get; set; }
        public int? StatusId { get; set; }
        public int? ResultId { get; set; }

        public bool ShowOnlyOwnCases { get; set; }


        public CaseFilterBase()
        {
            GateId = 1; //DPFGate - default and only
            ShowOnlyOwnCases = true;
        }
    }
}
