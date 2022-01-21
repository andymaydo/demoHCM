using System;

namespace HCM.Models
{
    public class CaseFilterModel
    {
        public DateTime? CreateFromDate { get; set; }
        public DateTime? CreateToDate { get; set; }
        public DateTime? ModifyFromDate { get; set; }
        public DateTime? ModifyToDate { get; set; }
        
        public int? CaseId { get; set; }
        public string SearchedName { get; set; }

        public int GateId { get; set; }
        
        public int? ProfilId { get; set; }
        public int? CategoryId { get; set; }
        public int? StatusId { get; set; }
        public int? ResultId { get; set; }

        public bool ShowOnlyOwnCases { get; set; } = true;
        public bool ShowOnlyOpenCases { get; set; } = true;
    }
}
