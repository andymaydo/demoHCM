using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace Domain.Models
{
    [Serializable]
    [DataContract(Namespace = "DominoCMS")]
    public class Case : CaseModel
    {       

        public CaseOriginator Originator
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(CaseData);
                XmlElement root = doc?.DocumentElement;
                var benutzerNode = root?.SelectSingleNode("/Root/Tran/BENUTZER");

                var benutzerXml = benutzerNode?.OuterXml;
                var originator = benutzerXml?.FromXml<CaseOriginator>("BENUTZER");
                return originator;
                
            }
        }

        public CaseSrc SrcData
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(CaseData);
                XmlElement root = doc?.DocumentElement;
                var srcNode = (root?.SelectSingleNode("/Root/Tran/Src") != null) ? root?.SelectSingleNode("/Root/Tran/Src") : root?.SelectSingleNode("/Root/Batch/Src");

                var srcXml = srcNode?.OuterXml;
                var srcData = srcXml?.FromXml<CaseSrc>("Src");
                return srcData;

            }
        }

        public CaseSettings SettingsData
        {
            get
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(CaseData);
                    XmlElement root = doc?.DocumentElement;
                    var settingsNode = (root?.SelectSingleNode("/Root/Tran/Settings") != null) ? root?.SelectSingleNode("/Root/Tran/Settings") : root?.SelectSingleNode("/Root/Batch/Settings");

                    var settingsXml = settingsNode?.OuterXml;
                    var settingsData = settingsXml?.FromXml<CaseSettings>("Settings");
                    return settingsData;
                }
                catch (Exception e)
                {
                    return new CaseSettings();
                }

            }
        }

        public List<Contact> ParticipantsAsList
        {
            get
            {
                return ObjHelper.FromXml<List<Contact>>(Participants, "ContactList");
            }
            set
            {
                this.Participants = value.ObjToXml("ContactList");
            }            
        }

    }
}
