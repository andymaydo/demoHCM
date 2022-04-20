using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Win32;
using System.Collections;
using System.Reflection;


namespace HCMConfig
{
    public static class SystemConfig
    {
        public static bool IsKeyExists(string DpfService, string DfpKey)
        {
            bool isexists = false;

            try
            {
                /********************************************************
	            * XML OBJECT
	            * *****************************************************/
                XmlDocument _xmlsettings = new XmlDocument();
                _xmlsettings.PreserveWhitespace = true;


                /********************************************************
               * READ CONFIG FILE
               * *****************************************************/
                string _SystemSettingsXml = System.IO.Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) + @"\HcmSettings.xml";
                XmlTextReader xmlread = new XmlTextReader(_SystemSettingsXml);
                _xmlsettings.Load(xmlread);
                xmlread.Close();

                /********************************************************
               * SEARCH CURRENT KEY
               * *****************************************************/
                foreach (System.Xml.XmlNode node in _xmlsettings["root"][DpfService])
                {
                    if ((node.Name == "add") && (node.Attributes.GetNamedItem("key").Value == DfpKey))
                    {
                        isexists = true;
                        break;
                    }
                }

            }
            catch
            {
                return false;
            }

            return isexists;
        }

        public static string GetSetting(string DpfService, string DfpKey)
        {
            string _value = "";


            /********************************************************
	        * XML OBJECT
	        * *****************************************************/
            XmlDocument _xmlsettings = new XmlDocument();
            _xmlsettings.PreserveWhitespace = true;


            /********************************************************
               * READ CONFIG FILE
               * *****************************************************/
            string _SystemSettingsXml = System.IO.Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) + @"\HcmSettings.xml";
            XmlTextReader xmlread = new XmlTextReader(_SystemSettingsXml);
            _xmlsettings.Load(xmlread);
            xmlread.Close();


            /********************************************************
            * GET CURRENT KEY
            * *****************************************************/
            foreach (System.Xml.XmlNode node in _xmlsettings["root"][DpfService])
            {
                if ((node.Name == "add") && (node.Attributes.GetNamedItem("key").Value == DfpKey))
                {
                    _value = node.Attributes.GetNamedItem("value").Value;
                }
            }

            if (_value.Trim() == "")
                return "";

            return _value;
        }

        public static bool AddSetting(string DpfService, string DfpKey, string DfpValue)
        {
            bool _result = false;

            /********************************************************
	        * XML OBJECT
	        * *****************************************************/
            XmlDocument _xmlsettings = new XmlDocument();
            _xmlsettings.PreserveWhitespace = true;


            /********************************************************
           * READ CONFIG FILE
           * *****************************************************/
            string _SystemSettingsXml = System.IO.Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) + @"\HcmSettings.xml";
            XmlTextReader xmlread = new XmlTextReader(_SystemSettingsXml);
            _xmlsettings.Load(xmlread);

            //_xmlsettings.Save(_SystemSettingsXml);
            xmlread.Close();




            /********************************************************
            * GET CURRENT KEY
            * *****************************************************/
            foreach (System.Xml.XmlNode node in _xmlsettings["root"][DpfService])
            {
                if ((node.Name == "add") && (node.Attributes.GetNamedItem("key").Value == DfpKey))
                {
                    node.Attributes.GetNamedItem("value").Value = DfpValue;
                    _result = true;
                    break;
                }
            }




            return _result;
        }
    }
}
