using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HCMApi
{
    public class CMSAPI : ICMSAPI
    {
        private readonly ICase _iCase;
        private readonly ICaseEvent _iCaseEvent;
        private readonly ICMSEvent _iCMSEvent;
        public CMSAPI(ICase iCase, ICaseEvent iCaseEvent, ICMSEvent iCMSEvent)
        {
            _iCase = iCase;
            _iCaseEvent = iCaseEvent;
            _iCMSEvent = iCMSEvent;
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
        public async Task<Case> LoadCase(int caseID)
        {
            return await _iCase.Load(caseID);
        }
        #endregion

        #region Event
        public async Task<List<CaseEvent>> GetEventList(int caseID)
        {
            CaseEvent cEvent = new CaseEvent();
            return await _iCaseEvent.GetHistoryByCase(caseID);
        }
        //public CaseEvent LoadEvent(int eventID)
        //{
        //    CaseEvent cEvent = new CaseEvent(eventID);
        //    return cEvent;
        //}
        #endregion

        /**************************************************
         * DPF EVENTS
         * **********************************************/
        #region OnEvent
        public async Task OnEvent(string eventID, int IssuedByID, int caseID, XmlDocument xmlData, string EventText)
        {
            Case lCase = await _iCase.Load(caseID);
            await runOnEvent(eventID, IssuedByID, lCase, xmlData, EventText, new List<CaseContact>());
        }
        public async Task OnEvent(string eventID, int IssuedByID, int caseID, XmlDocument xmlData, string EventText, List<CaseContact> xmlNotifyList)
        {
            Case lCase = await _iCase.Load(caseID);
            await runOnEvent(eventID, IssuedByID, lCase, xmlData, EventText, xmlNotifyList);
        }
        public async Task OnEvent(string eventID, int IssuedByID, int caseID, XmlDocument xmlData, string EventText, bool NotifyParticipants)
        {
            Case lCase = await _iCase.Load(caseID);
            if (NotifyParticipants)
                await runOnEvent(eventID, IssuedByID, lCase, xmlData, EventText, lCase.ParticipantsAsList);
            else
                await runOnEvent(eventID, IssuedByID, lCase, xmlData, EventText, new List<CaseContact>());

        }
        private async Task runOnEvent(string eventID, int IssuedByID, Case lCase, XmlDocument xmlData, string EventText, List<CaseContact> NotifyList)
        {           
            CaseEvent lCaseEvent = new CaseEvent(_iCaseEvent.GetIConfiguration());
            lCaseEvent.caseID = lCase.CaseID;
            lCaseEvent.IssuedByID = IssuedByID;
            lCaseEvent.eventID = eventID;
            lCaseEvent.CaseEventData = xmlData.InnerXml;
            lCaseEvent.CaseEventText = EventText;
            lCaseEvent.CaseEventNotifyContacts = CaseContactList.AsXmlStringFromList(NotifyList);

            await onEvent(lCase, lCaseEvent, IssuedByID);
        }
        public async Task<CMSEvent> LoadCMSEvent(string _eventID)
        {
            return await _iCMSEvent.Load(_eventID);
        }
        protected async Task<bool> onEvent(Case _case, CaseEvent caseEvent, int IssuedByID)
        {
            if (_case.CaseID < 1)
            {
                CMSEvent cmsEvent = await LoadCMSEvent(caseEvent.eventID);
                _case.CaseResultID = cmsEvent.CaseResultID;
                _case.CaseStatusID = cmsEvent.CaseStatusID;
                _case.CaseID = await _case.Create();
            }
            caseEvent.caseID = _case.CaseID;
            caseEvent.IssuedByID = IssuedByID;
            await caseEvent.Create();

            //await _iCaseEvent.Create();
            return true;

        }
        #endregion
    }
}
