using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[Serializable]
[DataContract(Namespace = "AliasApiWebSvc")]
[XmlRoot("authorization")]
public class AliasAuth
{
    [XmlElement("username")]
    public string username { get; set; }
    [XmlElement("pwd")]
    public string pwd { get; set; }
    [XmlElement("profileid")]
    public string profileid { get; set; }
    [XmlElement("sapip")]
    public string sapip { get; set; }
    [XmlElement("sapgw")]
    public string sapgw { get; set; }
    [XmlElement("sapmandant")]
    public string sapmandant { get; set; }
    [XmlElement("saptranid")]
    public string saptranid { get; set; }

	public AliasAuth()
	{
        username = String.Empty;
        pwd = String.Empty;

        profileid = String.Empty;
        sapip = String.Empty;
        sapgw = String.Empty;
        sapmandant = String.Empty;
        saptranid = String.Empty;
	}

    public AliasAuth(string _username, string _pwd, string _profileid, string _sapip, string _sapgw, string _sapmandant, string _saptranid)
    {
        username = _username;
        pwd = _pwd;

        profileid = _profileid;
        sapip = _sapip;
        sapgw = _sapgw;

        sapmandant = _sapmandant;
        saptranid = _saptranid;
    }

    public string AsXml()
    {
        XmlDocument xDoc = ObjectXMLSerializer.SerializeObjectAsXmlDoc<AliasAuth>(this);
        return xDoc.DocumentElement.OuterXml;
    }
}