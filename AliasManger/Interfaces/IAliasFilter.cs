using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliasManger.Interfaces
{
    internal interface IAliasFilter
    {
        public string HcmProfileId { get; set; }
        public string VgsLicenseId { get; set; }
        public string CloudAppId { get; set; }
        public int StatusId { get; set; }

        public string Name { get; set; }
        public string Street { get; set; }

        public bool WaitAuth { get; set; }
        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }
        public bool AllTime { get; set; }
    }
}
