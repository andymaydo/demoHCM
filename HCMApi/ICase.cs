using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace HCMApi
{
    public interface ICase
    {
        int? appID { get; set; }
        string appName { get; set; }
        XmlDocument CaseData { get; set; }
        int CaseID { get; set; }
        string CaseResult { get; set; }
        int CaseResultID { get; set; }
        string CaseSource { get; set; }
        string CaseStatus { get; set; }
        int? CaseStatusID { get; set; }
        string CaseType { get; set; }
        int? CaseTypeID { get; set; }
        string ContactName { get; set; }
        DateTime CreateDate { get; set; }
        string CustomerName { get; set; }
        int IssuedByID { get; set; }
        DateTime LastActivity { get; set; }
        int MinDuration { get; set; }
        int ModifiedByID { get; set; }
        string ModifiedByName { get; set; }
        int ProfileID { get; set; }
        string ProfileName { get; set; }
        string SapUser { get; set; }
        string Subject { get; set; }

        Task<List<Case>> GetCasesList(int? filterCaseID, int? appID, int? caseTypeID, int? caseStatusID, int loginUserID, DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? ProfileID, string CustomerName);
    }
}