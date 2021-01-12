using System;
using System.Collections.Generic;
using System.Text;

namespace HCMDataAccess.Models
{
    public class FiltersModels
    {
        public class FilterGateModel
        {
            public string appID { get; set; }
            public string appName { get; set; }
        }
        public class FilterProfileModel
        {
            public int ProfileID { get; set; }
            public string ProfileName { get; set; }
        }
        public class FilterCategoryModel
        {
            public int CaseTypeID { get; set; }
            public string CaseType { get; set; }
        }
        public class FilterStatusModel
        {
            public int CaseStatusID { get; set; }
            public string CaseStatus { get; set; }
        }
    }
}
