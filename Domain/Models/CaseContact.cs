using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Domain.Models
{
    public class Contact 
    {
        [XmlAttribute(AttributeName = "ContactID")]
        public int ContactID { get; set; }
        [XmlElement("Email")]
        public string Email { get; set; }
        [XmlElement("ContactURI")]
        public string ContactURI { get; set; }
        [XmlElement("ForeingID")]
        public string ForeingID { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("NickName")]
        public string NickName { get; set; }
        [XmlElement("ContactData")]
        public string ContactData { get; set; }
        [XmlElement("ProfileRole")]
        public string ProfileRole { get; set; }
        [XmlElement("Function")]
        public string Function { get; set; }
    }

}
