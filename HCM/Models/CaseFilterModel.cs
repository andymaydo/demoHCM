using Domain.Models;
using System;

namespace HCM.Models
{
    public class CaseFilterModel : CaseFilterBase
    {
        public bool ShowOwnCasesCheckBox { get; set; }
        public bool ShowOnlyOpenCases { get; set; }
        public bool ShowStatus { get; set; }
        public bool ShowResult { get; set; }

        public CaseFilterModel(bool showOnlyOpenCases = true, bool showStatus = true, 
            bool showResult = true, bool  showOwnCasesCheckBox = false) : base()
        {
            ShowOwnCasesCheckBox = showOwnCasesCheckBox;

            ShowOnlyOpenCases = showOnlyOpenCases;
            ResultId = ShowOnlyOpenCases ? 101 : null;

            ShowStatus = showStatus;    
            ShowResult = showResult;
        }
    }
}
