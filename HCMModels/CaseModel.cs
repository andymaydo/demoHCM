using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace HCMModels
{
    [Serializable]
    [DataContract(Namespace = "DominoCMS")]
    public class CaseModel
    {
        public int appID { get; set; }
        public string appName { get; set; }
        public string CaseData { get; set; }
        //public XmlDocument CaseData { get; set; }

        public int CaseID { get; set; }
        public string CaseResult { get; set; }
        public int CaseResultID { get; set; }
        public string CaseSource { get; set; }
        public string CaseStatus { get; set; }
        public int CaseStatusID { get; set; }
        public int CaseCount { get; set; }
        public string CaseType { get; set; }
        public int? CaseTypeID { get; set; }
        public string ContactName { get; set; }
        public int ContactID { get; set; }
        public DateTime CreateDate { get; set; }
        public string CustomerName { get; set; }
        public int IssuedID { get; set; }
        public DateTime LastActivity { get; set; }
        public int Duration { get; set; }
        public int ModifiedByID { get; set; }
        public string ModifiedByName { get; set; }    
        public int ProfileID { get; set; }
        public string ProfileName { get; set; }
        public XmlDocument profileParticipants { get; set; }
        public string EscalationUsers { get; set; }
        public string SapUser { get; set; }
        public string Subject { get; set; } 
        public string AliasName { get; set; }
        
        //public XmlDocument Participants { get; set; }
        
         
        public string Participants { get; set; }
        public XmlDocument ParticipantsAsXml { get; set; }
        
        public XmlDocument CaseDataAsXml 
        { get; set; }

        
        public class CaseEventStatus
        {
            public string EventID { get; set; }
            public string EventText { get; set; }
        }

        public class AliasLizStatus
        {
            public string LizID { get; set; }
            public string LizText { get; set; }
        }
        

    }
}
