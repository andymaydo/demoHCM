using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization; 

/// <summary>
/// Summary description for ObjectXMLSerializer
/// </summary>
public static class ObjectXMLSerializer
{
    /// <summary>
    /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
    /// </summary>
    /// <param name="characters">Unicode Byte Array to be converted to String</param>
    /// <returns>String converted from Unicode Byte Array</returns>
    private static string UTF8ByteArrayToString(byte[] characters)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        string constructedString = encoding.GetString(characters);
        return (constructedString);
    }

    /// <summary>
    /// Converts the String to UTF8 Byte array and is used in De serialization
    /// </summary>
    /// <param name="pXmlString"></param>
    /// <returns></returns>
    private static byte[] StringToUTF8ByteArray(string pXmlString)
    {
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] byteArray = encoding.GetBytes(pXmlString);
        return byteArray;
    }

    /// <summary>
    /// Serialize an object into an XML string
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string SerializeObject<T>(T obj)
    {
        try
        {
            string xmlString = null;
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializerNamespaces xmlnsEmpty = new XmlSerializerNamespaces();
            xmlnsEmpty.Add("", "");
            XmlSerializer xs = new XmlSerializer(typeof(T));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            xs.Serialize(xmlTextWriter, obj, xmlnsEmpty);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            xmlString = UTF8ByteArrayToString(memoryStream.ToArray());
            return xmlString;
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// Reconstruct an object from an XML string
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static T DeserializeObject<T>(string xml)
    {
        XmlSerializer xs = new XmlSerializer(typeof(T));
        MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(xml));
        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        return (T)xs.Deserialize(memoryStream);
    }

    public static XmlDocument SerializeObjectAsXmlDoc<T>(T obj)
    {
        string encodedString = ObjectXMLSerializer.SerializeObject<T>(obj);
        // Put the byte array into a stream and rewind it to the beginning
        MemoryStream ms = new MemoryStream(StringToUTF8ByteArray(encodedString));
        ms.Flush();
        ms.Position = 0;

        // Build the XmlDocument from the MemorySteam of UTF-8 encoded bytes
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(ms);

        return xmlDoc;
    }
}