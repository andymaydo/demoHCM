using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain.Models
{
    [Serializable]
    [DataContract(Namespace = "DominoCMS")]
    [XmlRootAttribute("CaseDocument")]
    public class CaseDoc
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
    }

    public class CaseDocs
    {
        [XmlArrayItem("CaseDocument")]
        public List<CaseDoc> CaseDocList { get; set; }
        public string AddReport { get; set; }

        public CaseDocs()
        {
            CaseDocList = new List<CaseDoc>();
        }
    }
}
