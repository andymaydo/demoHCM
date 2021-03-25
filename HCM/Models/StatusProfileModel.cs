using HCMApi;
using HCMApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCM.Models
{
    public class StatusProfileModel
    {
        public int Step { get; set; }       // до коя стъпке е създаден профила        
        public CMSProfileModel cmsProfile { get; set; }
    }
}
