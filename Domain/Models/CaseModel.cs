using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace Domain.Models
{
    [Serializable]
    [DataContract(Namespace = "DominoCMS")]
    public class CaseModel
    {
        public int appID { get; set; }
        public string appName { get; set; }
        public string CaseData { get; set; }
     
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
        
        
         
        public string Participants { get; set; }
        public XmlDocument ParticipantsAsXml { get; set; }
        
        public XmlDocument CaseDataAsXml { get; set; }

        
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

        public class CaseOriginator
        {
            public string USERNAME { get; set; }
            public string NAMEFIRST { get; set; }
            public string NAMELAST { get; set; }
            public string LANGUAGE { get; set; }
            public string TELNR { get; set; }
            public string TELEXT { get; set; }
            public string MOBEXT { get; set; }
            public string EMAIL { get; set; }
        }

        public class CaseSrc
        {
            public string clid { get; set; }
            public string sourceid { get; set; }
            public string name { get; set; }
            public string street { get; set; }
            public string city { get; set; }
            public string zip { get; set; }
            public string country { get; set; }
            public string vbeln { get; set; }
            public int tranid { get; set; }
            public int index { get; set; }
            public int res { get; set; }
        }

        public class CaseSettings
        {
            public int profileid { get; set; }
            public string profilename { get; set; }
            public string sapip { get; set; }
            public string sapgw { get; set; }
            public string sapmandant { get; set; }
            public string saprfcdest { get; set; }
            public int batchid { get; set; }
            public int tranid { get; set; }
            public string startedon { get; set; }
            public string finishedon { get; set; }
            public string denlists { get; set; }
            public int verid { get; set; }
            public string checktype { get; set; }
            public string matchtype { get; set; }
            public int redalert { get; set; }
            public int totalrecords { get; set; }
            public int matchedrecord { get; set; }
            public int totalmatches { get; set; }
            public int maxmatchprcnt { get; set; }
            public string sapbk { get; set; }
            public string sapvo { get; set; }
            public string sapwk { get; set; }
            public string saptrn { get; set; }
            public string sapusr { get; set; }
            public string sapvar { get; set; }
            public int matchtypeid { get; set; }
            public int checktypeid { get; set; }
            public int startverid { get; set; }
        }
    }
}
