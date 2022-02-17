using HCMApi;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace HCM.Models
{
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
