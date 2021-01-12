using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCMApi
{
    public interface ICMSAPI
    {
        Task<List<Case>> GetCaseList(int? caseID, int loginUserID);
        Task<List<Case>> GetCaseList(int? caseID, int? appID, int? caseTypeID, int? caseStatusID, int loginUserID, DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? ProfileID, string CustomerName);
    }
}