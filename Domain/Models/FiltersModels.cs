using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class FiltersModels
    {
        public class GateModel
        {
            public int appID { get; set; }
            public string appName { get; set; }
        }
        public class ProfileModel
        {
            public int ProfileID { get; set; }
            public string ProfileName { get; set; }
        }
        public class CategoryModel
        {
            public int CaseTypeID { get; set; }
            public string CaseType { get; set; }
        }
        public class CaseStatusModel
        {
            public int CaseStatusID { get; set; }
            public string CaseStatus { get; set; }
        }

        public class CaseResultModel
        {
            public int CaseResultID { get; set; }
            public string CaseResult { get; set; }
        }

        
        public class ProfileStatusModel
        {
            public int ProfileStatusID { get; set; }
            public string ProfileStatus { get; set; }
        }

        public class ReportFilterModel
        {
            public int? CaseID { get; set; }
            public int? ContactID { get; set; }
            public string CustomerName { get; set; }
        }
    }
}
