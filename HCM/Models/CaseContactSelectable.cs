using Domain.Models;
using HCMApi;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace HCM.Models
{
    [Serializable]
    [XmlRoot("Contact")]
    public class CaseContactSelectable : Contact
    {
        [XmlIgnore]
        public bool Selected { get; set; }
    }

    //public class CaseContactList
    //{
    //    [XmlArrayItem("Contact")]
    //    public List<CaseContact> ContactList { get; set; }
    //}
}
