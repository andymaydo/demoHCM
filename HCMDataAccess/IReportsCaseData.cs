using HCMModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HCMDataAccess
{
    public interface IReportsCaseData
    {
        Task<List<CaseModel>> CaseDetail(int appID, int? CaseTypeID, int? ContactID, int? StatusID, int? ResultID,
        DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? CaseID, int? ProfileID, string CustomerName);

        Task<List<CaseModel>> CaseByStatus(int appID, int? CaseTypeID, int? ContactID,
            DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? CaseID, int? ProfileID, string CustomerName);

        Task<List<CaseModel>> CaseByResult(int appID, int? CaseTypeID, int? ContactID,
            DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? CaseID, int? ProfileID, string CustomerName);
        Task<List<CaseModel>> AliasCount(int appID, int? CaseTypeID, int? ContactID,
            DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? CaseID, int? ProfileID, string CustomerName);

        Task<List<CaseModel>> AliasDetail(int appID, int? CaseTypeID, int? ContactID, int? StatusID, int? ResultID,
            DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? CaseID);
    }
}