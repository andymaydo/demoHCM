using HCMDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static HCMDataAccess.Models.ReportsModels;

namespace HCMDataAccess
{
    public interface IReportsCaseData
    {
        Task<List<ReportCaseByStatusModels>> CaseByStatus(int appID, int? CaseTypeID, int? ContactID,
            DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? CaseID, int? ProfileID, string CustomerName);

        Task<List<ReportCaseByResultModels>> CaseByResult(int appID, int? CaseTypeID, int? ContactID,
            DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? CaseID, int? ProfileID, string CustomerName);
        Task<List<ReportAliasCountModels>> AliasCount(int appID, int? CaseTypeID, int? ContactID,
            DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? CaseID, int? ProfileID, string CustomerName);
    }
}