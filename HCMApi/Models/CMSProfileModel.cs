using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace HCMApi.Models
{
    public class CMSProfileModel
    {
        public int profileID { get; set; }
        public int appID { get; set; }
        public string appName { get; set; }
        public int profileStatusID { get; set; }
        public string profileStatus { get; set; }     
        public int profileType { get; set; }

        public string profileName { get; set; }
        public string profilDescr { get; set; }
        public string NotificationLang { get; set; }

        public List<CaseContact> profileParticipants { get; set; }
        public List<CaseContact> escalationUsers { get; set; }
        public List<CaseRule> escalationRules { get; set; }
        public bool NotifyAllProfileParticipants { get; set; }

        public string profileURI { get; set; }
        public string WebURL { get; set; }
        public string WebAuthenitcationType { get; set; }
        public int ModifiedBy { get; set; }
        public string ReasonToDelete { get; set; }

        public string ProfileParticipants_AsXmlString()
        {
            XmlDocument xDoc = new XmlDocument();
            string s = @"<ContactList>";
            foreach (CaseContact key in profileParticipants)
            {
                s += key.AsXml();
            }
            s += @"</ContactList>";
            return s;
        }
        public string EscalationUsers_AsXmlString()
        {
            XmlDocument xDoc = new XmlDocument();
            string s = @"<ContactList>";
            foreach (CaseContact key in escalationUsers)
            {
                s += key.AsXml();
            }
            s += @"</ContactList>";
            return s;
        }
        public string EscalationRules_AsXmlString()
        {
            XmlDocument xDoc = new XmlDocument();
            string s = @"<CaseRuleList>";
            foreach (CaseRule key in escalationRules)
            {
                s += key.AsXml();
            }
            s += @"</CaseRuleList>";
            return s;
        }
    }

     public class CMSProfileModelSimple
    {
        public int profileID { get; set; }
        public int appID { get; set; }
        public string appName { get; set; }
        public int profileStatusID { get; set; }
        public string profileStatus { get; set; }
        public int profileType { get; set; }

        public string profileName { get; set; }
        public string profilDescr { get; set; }
        public string NotificationLang { get; set; }

        public string profileParticipants { get; set; }
        public string escalationUsers { get; set; }
        public string escalationRules { get; set; }
        public bool NotifyAllProfileParticipants { get; set; }

        public string profileURI { get; set; }
        public string WebURL { get; set; }
        public string WebAuthenitcationType { get; set; }
        public int ModifiedBy { get; set; }
        public string ReasonToDelete { get; set; }
    }
}
