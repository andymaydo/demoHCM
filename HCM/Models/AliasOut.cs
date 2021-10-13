using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for AliasOut
/// </summary>
/// 

[Serializable]
[DataContract(Namespace = "DublicateWebSvc")]
[XmlRoot("root")]
public class AliasOut
{
    [XmlElement("authorization")]
    public AliasAuth aliasauth { get; set; }
    [XmlElement("data")]
    public AliasData data { get; set; }

	public AliasOut()
	{
        aliasauth = new AliasAuth();
        data = new AliasData();
	}

    public string AsXml()
    {
        XmlDocument xDoc = ObjectXMLSerializer.SerializeObjectAsXmlDoc<AliasOut>(this);
        return xDoc.DocumentElement.OuterXml;
    }
}