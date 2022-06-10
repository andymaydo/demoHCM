using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml;

namespace Domain.Models
{
    public class UsersModel
    {
        public int UserID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public int ContactID { get; set; }
        public int Enable { get; set; }
        public string EMail { get; set; }
        public string FullName { get; set; }

        public string FullNameEmailFunction 
        { 
            get
            {
                return Name + " (" + EMail + ") " + FunctionInFirma;
            }
        }
        public string Name { get; set; }
        public string NickName { get; set; }
        public XmlDocument ContactData { get; set; }
        public string FunctionInFirma { get; set; }

        public bool Status { 
            get
            {
                return Enable == 1 ? true:false;
            }
        }
    }
}
