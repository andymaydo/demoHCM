using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace HCMApi
{
    public interface ICMSAPI
    {
        Task<List<Case>> GetCaseList(int? caseID, int loginUserID);
        Task<List<Case>> GetCaseList(int? caseID, int? appID, int? caseTypeID, int? caseStatusID, int loginUserID, DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? ProfileID, string CustomerName);
        Task<Case> LoadCase(int caseID);

        Task<List<CaseEvent>> GetEventList(int caseID);

        Task OnEvent(string eventID, int IssuedByID, int caseID, XmlDocument xmlData, string EventText, bool NotifyParticipants);
        Task OnEvent(string eventID, int IssuedByID, int caseID, XmlDocument xmlData, string EventText, List<CaseContact> xmlNotifyList);
    }
}