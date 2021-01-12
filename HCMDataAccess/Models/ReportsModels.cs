using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace HCMDataAccess.Models
{
    public class ReportsModels
    {
        public class ReportCaseDetailModels
        {
            public int caseID { get; set; }
            public int CaseTypeID { get; set; }
            public string CaseType { get; set; }
            public int appID { get; set; }
            public string appName { get; set; }
            public int CaseStatusID { get; set; }
            public string CaseStatus { get; set; }
            public int CaseResultID { get; set; }
            public string CaseResult { get; set; }
            public DateTime CreateDate { get; set; }
            public int profileID { get; set; }
            public string profileName { get; set; }
            public XmlDocument profileParticipants { get; set; }
            public string EscalationUsers { get; set; }
            public DateTime LastActivity { get; set; }
            public string Subject { get; set; }
            public XmlDocument CaseData { get; set; }
            public XmlDocument Participants { get; set; }
            public string ContactName { get; set; }
            public int ContactID { get; set; }
            public int IssuedID { get; set; }
            public int ModifiedByID { get; set; }
            public string ModifiedByName { get; set; }
            public string CaseSource { get; set; }
            public int Duration { get; set; }
            public string CustomerName { get; set; }
            public string SAPUser { get; set; }
        }

        public class ReportCaseByStatusModels
        {
            public string CaseStatus { get; set; }
            public int CaseStatusID { get; set; }
            public int CaseCount { get; set; }
        }

        public class ReportCaseByResultModels
        {
            public string CaseResult { get; set; }
            public int CaseResultID { get; set; }
            public int CaseCount { get; set; }
        }

        public class ReportAliasCountModels
        {
            public int CaseCount { get; set; }
        }

        public class ReportAliasDetailModels
        {
            public int CaseID { get; set; }
            public string CaseType { get; set; }
            public int CaseStatusID { get; set; }
            public string AliasName { get; set; }
            public string ContactName { get; set; }
            public DateTime CreateDate { get; set; }
        }
    }
}
