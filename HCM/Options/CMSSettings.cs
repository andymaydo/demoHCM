using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCM.Options
{
    public class CMSSettings
    {
        public string CmsDocPath { get; set; }
        public string VGSAccId { get; set; }
        public string AliasApiUrl { get; set; }
        public string AliasApiUsr { get; set; }
        public string AliasApiPwd { get; set; }

        public static string EventAddMessage       = "cms.addnote";
        public static string EventNotifyContact    = "cms.notifycontact";
        public static string EventParticipantsChanged = "cms.participantschanged";
        public static string EventDpfChecking      = "dpf.checking";
        public static string EventDpfEscalation    = "dpf.escalation";
        public static string EventDpfNoMatch       = "dpf.nomatch";
        public static string EventDpfOnMatchFound  = "dpf.onmatchfound";
        public static string EventDpfYesMatch      = "dpf.yesmatch";
        public static string EventAddAlias          = "dpf.addalias";
    }


}
