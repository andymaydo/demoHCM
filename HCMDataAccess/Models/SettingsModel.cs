using System;
using System.Collections.Generic;
using System.Text;

namespace HCMDataAccess.Models
{
    public class SettingsModel
    {
        public string SMTPServer { get; set; }
        public string SMTPServerPort { get; set; }
        public bool SMTPServerAuth { get; set; }
        public string SMTPServerUser { get; set; }
        public string SMTPServerPass { get; set; }
    }
}
