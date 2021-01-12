using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMApi
{
    public class CMSAPI : ICMSAPI
    {
        private readonly ICase _iCase;
        public CMSAPI(ICase iCase)
        {
            _iCase = iCase;
        }

        #region Case
        public async Task<List<Case>> GetCaseList(int? caseID, int loginUserID)
        {
            return await _iCase.GetCasesList(caseID, null, null, null, loginUserID, null, null, null, null, null, null);
        }
        public async Task<List<Case>> GetCaseList(int? caseID, int? appID, int? caseTypeID, int? caseStatusID, int loginUserID,
            DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? ProfileID, string CustomerName)
        {
            return await _iCase.GetCasesList(caseID, appID, caseTypeID, caseStatusID, loginUserID, CreateDate1, CreateDate2, ModifiedDate1, ModifiedDate2, ProfileID, CustomerName);
        }
        //public Case LoadCase(int caseID)
        //{
        //    Case lCase = new Case(caseID);
        //    return lCase;
        //}
        #endregion
    }
}
