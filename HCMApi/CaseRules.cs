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

namespace HCMApi
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
        public static CaseRule GetContactFromXML(string xml)
        {
            return ObjectXMLSerializer.DeserializeObject<CaseRule>(xml);
        }
        public string AsXml()
        {
            XmlDocument xDoc = ObjectXMLSerializer.SerializeObjectAsXmlDoc<CaseRule>(this);
            return xDoc.DocumentElement.OuterXml;

        }
    }

    /// <summary>
    /// class for storing collection of  CaseRule items
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "DominoCMS")]
    public class CaseRuleList : Hashtable
    {
        public CaseRuleList() { }

        protected CaseRuleList(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public void CopyTo(Exception[] array, int index)
        {
            base.CopyTo(array, index);
        }
        public override bool Contains(Object key)
        {
            return base.Contains(key.ToString().ToUpper());
        }

        public static CaseRuleList LoadFromXml(string xml)
        {
            CaseRuleList _rules = new CaseRuleList();
            if (!String.IsNullOrEmpty(xml))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xml);
                foreach (XmlNode nCaseRule in xDoc.SelectNodes(@"//CaseRule"))
                {
                    CaseRule c = CaseRule.GetContactFromXML(nCaseRule.OuterXml);
                    _rules.Add(c.CaseStatusID, c);
                }
            }
            return _rules;
        }
         public static List<CaseRule> LoadFromXmlAsList(string xml)
        {
            List<CaseRule> _CaseRuleList = new List<CaseRule>();
            if (!String.IsNullOrEmpty(xml))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xml);
                foreach (XmlNode nCaseRule in xDoc.SelectNodes(@"//CaseRule"))
                {
                    CaseRule c = CaseRule.GetContactFromXML(nCaseRule.OuterXml);
                    _CaseRuleList.Add(c);
                }
            }
            return _CaseRuleList;
        }
        public XmlDocument AsXml()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(AsXmlString());
            return xDoc;
        }
        public string AsXmlString()
        {
            XmlDocument xDoc = new XmlDocument();
            string s = @"<CaseRuleList>";
            foreach (int key in base.Keys)
            {
                s += ((CaseRule)base[key]).AsXml();
            }
            s += @"</CaseRuleList>";
            return s;
        }
    }
}
