using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
/// <summary>
/// Summary description for AliasData
/// </summary>
/// 

[Serializable]
[DataContract(Namespace = "AliasApiWebSvc")]
[XmlRoot("data")]
public class AliasData
{
    [XmlElement("aliasname")]
    public string aliasname { get; set; }
    [XmlElement("aliasaddress")]
    public string aliasaddress { get; set; }
    [XmlElement("realname")]
    public string realname { get; set; }
    [XmlElement("realaddress")]
    public string realaddress { get; set; }
    [XmlElement("aliasnote")]
    public string aliasnote { get; set; }
    [XmlElement("verid")]
    public int verid { get; set; }
    [XmlElement("caseurl")]
    public string caseurl { get; set; }
    [XmlElement("hcmUserFullName")]
    public string hcmUserFullName { get; set; }

    //[XmlElement("appid")]
    //public string appid { get; set; }
    

	public AliasData()
	{
        aliasname = String.Empty;
        aliasaddress = String.Empty;

        realname = String.Empty;
        realaddress = String.Empty;
        aliasnote = String.Empty;
        verid = -1;
        caseurl = String.Empty;
        hcmUserFullName = String.Empty;

        //appid = String.Empty;
	}


    public AliasData(string _aliasname, string _aliasaddress, string _realname, string _realaddress, string _aliasnote, int _verid, string _caseurl, string _appid)
    {
        aliasname = _aliasname;
        aliasaddress = _aliasaddress;

        realname = _realname;
        realaddress = _realaddress;
        aliasnote = _aliasnote;
        verid = _verid;
        caseurl = _caseurl;
        //appid = _appid;
    }

    public string AsXml()
    {
        XmlDocument xDoc = ObjectXMLSerializer.SerializeObjectAsXmlDoc<AliasData>(this);
        return xDoc.DocumentElement.OuterXml;
    }
}