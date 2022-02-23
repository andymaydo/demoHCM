using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace HCMModels
{
    [Serializable]
    [DataContract(Namespace = "DominoCMS")]
    [XmlRoot("Contact")]

    public class CaseContactModel
    {
        [XmlAttribute("ContactID")]
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
