using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMApi
{
    public static class CaseEventHandlers
    {
        public static bool OnEvent(CaseEvent cEvent)
        {
            switch (cEvent.eventID)
            {
                case "cms.participantschanged":
                    cms_participantschanged(cEvent);
                    break;
                case "dpf.checking":
                    dpf_checking(cEvent);
                    break;
                case "default":
                    break;
            }

            SendMailToNotifyList(cEvent);
            return true;
        }

        static bool SendMailToNotifyList(CaseEvent cEvent)
        {
            //if (cEvent.CaseEventNotifyContacts.Count > 0)
                cEvent.SendMailToNotifyList();

            return true;
        }

        static bool dpf_checking(CaseEvent cEvent)
        {

            return true;
        }

        static bool cms_participantschanged(CaseEvent cEvent)
        {
            Case lCase = new Case(cEvent.GetIConfiguration());
            lCase.UpdateParticipants(cEvent.caseID, cEvent.CaseEventData);
            return true;
        }
    }
}
