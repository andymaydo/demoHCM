using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Domain;

namespace Domain.Models
{
    [Serializable]
    [DataContract(Namespace = "DominoCMS")]
    [XmlRootAttribute("CaseRule")]
    public class CaseRule
    {
        [XmlElement("CaseStatusID")]
        public int CaseStatusID { get; set; }
        [XmlElement("CaseStatusName")]
        public string CaseStatusName { get; set; }
        [XmlElement("CaseStatusDays")]
        public int CaseStatusDays { get; set; }
        [XmlElement("CaseRulesNote")]
        public string CaseRulesNote { get; set; }

        public CaseRule()
        {
        }

    }

}
