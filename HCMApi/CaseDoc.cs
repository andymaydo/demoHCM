using HCMDataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HCMApi
{
    [Serializable]
    [DataContract(Namespace = "DominoCMS")]
    [XmlRootAttribute("CaseDocument")]
    public class CaseDoc:ICaseDoc
    {
        [XmlAttribute("DocID")]
        public string DocID { get; set; }
        [XmlAttribute("CaseID")]
        public int CaseID { get; set; }
        [XmlAttribute("DocSize")]
        public Int64 DocSize { get; set; }
        [XmlAttribute("FilePath")]
        public string FilePath { get; set; }
        [XmlAttribute("ContactID")]
        public int ContactID { get; set; }
        [XmlAttribute("ContactName")]
        public string ContactName { get; set; }
        [XmlAttribute("CreatedOn")]
        public DateTime CreatedOn { get; set; }
        [XmlAttribute("Email")]
        public string Email { get; set; }
        [XmlAttribute("DocName")]
        public string DocName { get; set; }

        [XmlAttribute("OrgFileName")]
        public string OrgFileName { get; set; }

        private readonly IConfiguration _config;
        private readonly ISqlDataAccess _db;
        public CaseDoc(IConfiguration config, ISqlDataAccess db)
        {
            _config = config;
            _db = db;
        }

         public CaseDoc() { }

        public string AsXml()
        {
            XmlDocument xDoc = ObjectXMLSerializer.SerializeObjectAsXmlDoc<CaseDoc>(this);
            return xDoc.DocumentElement.OuterXml;

        }

        public CaseDoc GetDocFromXML(string xml)
        {
            return ObjectXMLSerializer.DeserializeObject<CaseDoc>(xml);
        }

        public List<CaseDoc> LoadFromXml(string xml)
        {
            List<CaseDoc> _CaseDocList = new List<CaseDoc>();
            if (!String.IsNullOrEmpty(xml))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xml);
                foreach (XmlNode nDoc in xDoc.SelectNodes(@"//CaseDocument"))
                {
                    CaseDoc d = GetDocFromXML(nDoc.OuterXml);
                    _CaseDocList.Add(d);
                }
            }
            return _CaseDocList;
        }

    }
}
