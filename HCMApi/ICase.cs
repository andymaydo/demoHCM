using HCMModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using static HCMApi.Case;

namespace HCMApi
{
    public interface ICase
    {
        Task<List<Case>> GetCasesList(int? filterCaseID, int? appID, int? caseTypeID, int? caseStatusID, int loginUserID, DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? ProfileID, string CustomerName);
        Task<Case> Load(int _caseID);

        Task<int> Create();

        int UpdateParticipants(int _caseID, string _participants);

        Task<List<CaseModel.CaseEventStatus>> GetStatusByCase(int caseID);

        }
}