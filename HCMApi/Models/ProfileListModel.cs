using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMApi.Models
{
    public class ProfileListModel
    {
        public int profileID { get; set; }
        public int appID { get; set; }
        public string appName { get; set; }
        public int profileStatusID { get; set; }
        // public string profileStatus { get; set; }
        public int profileType { get; set; }

        public string profileName { get; set; }
        public string profilDescr { get; set; }
        public string EmailLanguage { get; set; }
    }
}
