using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMApi
{
    public interface ICaseDoc
    {
        public string DocID { get; set; }
        public int CaseID { get; set; }
        public Int64 DocSize { get; set; }
          public string FilePath { get; set; }
         public int ContactID { get; set; }

        public string ContactName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Email { get; set; }

        public string DocName { get; set; }

        string AsXml();
        CaseDoc GetDocFromXML(string xml);
        List<CaseDoc> LoadFromXml(string xml);
    }
}
