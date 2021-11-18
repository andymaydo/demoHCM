using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HCMModels
{
    public class SettingsModel
    {
        
        public string SMTPServer { get; set; }
        public string SMTPServerPort { get; set; }
        public bool SMTPServerAuth { get; set; }
        public string SMTPServerUser { get; set; }
        public string SMTPServerPass { get; set; }

        public string AppUrl { get; set; }
    }
}
