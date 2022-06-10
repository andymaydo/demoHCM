using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Domain
{
    public class XslHelper
    {
        public class HCMXslTransform
        {
            public static string GetHtmlResult(string xmlData, int caseTypeID)
            {
                string myMarkup="";

                try { 

                    if (!string.IsNullOrEmpty(xmlData))
                    {
                    
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(xmlData);

                        XmlReader reader = XmlReader.Create(new StringReader(doc.SelectSingleNode("Root").InnerXml));
                        XPathDocument myXPathDoc = new XPathDocument(reader);
                        XslCompiledTransform _xslcompiled = new XslCompiledTransform();

                        string _xslfilename = "";
                        if (caseTypeID == 0)
                        {
                            _xslfilename = "DpfMatchData_de-DE.xslt";
                            if (CultureInfo.CurrentCulture.Name == "en-US")
                            {
                                _xslfilename = "DpfMatchData_en-US.xslt";
                            }
                        }
                        else
                        {
                            switch (caseTypeID)
                            {
                                case 1:
                                    _xslfilename = "RT.de-DE.xslt";
                                    if (CultureInfo.CurrentCulture.Name == "en-US")
                                    {
                                        _xslfilename = "RT.en-US.xslt";
                                    }
                                    break;
                                case 2:
                                    _xslfilename = "OnDemand.de-DE.xslt";
                                    if (CultureInfo.CurrentCulture.Name == "en-US")
                                    {
                                        _xslfilename = "OnDemand.en-US.xslt";
                                    }
                                    break;
                                case 3:
                                    _xslfilename = "DeltaBatch.de-DE.xslt";
                                    if (CultureInfo.CurrentCulture.Name == "en-US")
                                    {
                                        _xslfilename = "DeltaBatch.en-US.xslt";
                                    }
                                    break;
                                case 4:
                                    _xslfilename = "Perpetuum.de-DE.xslt";
                                    if (CultureInfo.CurrentCulture.Name == "en-US")
                                    {
                                        _xslfilename = "Perpetuum.en-US.xslt";
                                    }
                                    break;
                                case 5:
                                    _xslfilename = "RFC.de-DE.xslt";
                                    if (CultureInfo.CurrentCulture.Name == "en-US")
                                    {
                                        _xslfilename = "RFC.en-US.xslt";
                                    }
                                    break;
                                case 6:
                                    _xslfilename = "RFCFullBatch.de-DE.xslt";
                                    if (CultureInfo.CurrentCulture.Name == "en-US")
                                    {
                                        _xslfilename = "RFCFullBatch.en-US.xslt";
                                    }
                                    break;
                                case 7:
                                    _xslfilename = "RFCDeltaBatch.de-DE.xslt";
                                    if (CultureInfo.CurrentCulture.Name == "en-US")
                                    {
                                        _xslfilename = "RFCDeltaBatch.en-US.xslt";
                                    }
                                    break;
                                case 10:
                                    _xslfilename = "RTPlus.de-DE.xslt";
                                    if (CultureInfo.CurrentCulture.Name == "en-US")
                                    {
                                        _xslfilename = "RTPlus.en-US.xslt";
                                    }
                                    break;
                                default:
                                    _xslfilename = "DpfMatchData_en-US.xslt";
                                    if (CultureInfo.CurrentCulture.Name == "en-US")
                                    {
                                        _xslfilename = "DpfMatchData_en-US.xslt";
                                    }
                                    break;
                            }

                        }

                        string _xslPath = Path.Combine((string)AppDomain.CurrentDomain.GetData("WebRootPath"), "xsl\\" + _xslfilename);
                        _xslcompiled.Load(_xslPath);

                        var memory = new MemoryStream();
                        _xslcompiled.Transform(myXPathDoc, GetArgumentList(), memory);
                        memory.Position = 0;
                        StreamReader reader1 = new StreamReader(memory);

                        if (reader.ReadState != ReadState.Closed)
                            reader.Close();

                        myMarkup = reader1.ReadToEnd();

                        reader1.Close();

                    
                    }
                }
                catch
                {
                    return myMarkup;
                }
                return myMarkup;
            }

            static XsltArgumentList GetArgumentList()
            {
                XslHelper.XSLFormat objXSLFormat = new XslHelper.XSLFormat();
                XsltArgumentList args = new XsltArgumentList();
                args.AddExtensionObject("urn:domino-function", objXSLFormat);

                return args;
            }
        }

        public class XSLFormat
        {



            /***********************************************************
            * XSL argument
            * ********************************************************/
            #region XSL argument
            public static XsltArgumentList GetArgumentList()
            {
                XSLFormat objXSLFormat = new XSLFormat();
                XsltArgumentList args = new XsltArgumentList();
                args.AddExtensionObject("urn:domino-function", objXSLFormat);

                return args;
            }
            #endregion


            /***********************************************************
             * datetiem function
             * ********************************************************/
            #region convert datetime
            public string GetDateTime(string data, string format)
            {
                try
                {
                    //CultureInfo culture = new CultureInfo( "en-US" );
                    //System.DateTime dt = DateTime.Parse( data, culture);

                    CultureInfo Invc = CultureInfo.InvariantCulture;
                    System.DateTime dt = DateTime.Parse(data, Invc);
                    return dt.ToString(format);
                }
                catch
                {
                    return "";
                }
            }
            #endregion


            /***********************************************************
             * functions - ReplaceEmpty, CheckMatchPercent, FixGUID,
             *				ReplaceStr, FixCurrency, FixBool
             * ********************************************************/
            #region common functions
            public string ReplaceEmpty(string strdata)
            {
                if (strdata.Trim().Length > 0)
                    return strdata.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\r\n", "<br>").Replace("\n", "<br>");
                else
                    return "-";
            }

            public bool CheckMatchPercent(string strdata)
            {
                bool ret = false;

                try
                {
                    if (Convert.ToDouble(strdata) > 0)
                        ret = true;
                    else
                        ret = false;
                }
                catch
                {
                    ret = false;
                }

                return ret;
            }

            public string FixGUID(string strdata)
            {
                return strdata.Replace("-", "");
            }

            public string ReplaceStr(string strdata, string oldvalue, string newvalue)
            {
                if (strdata.Trim().Length > 0)
                    return strdata.Replace(oldvalue.Replace("&lt;", "<").Replace("&gt;", ">"), newvalue);
                else
                    return "-";
            }

            public string FixCurrency(string strdata)
            {
                if (strdata.Trim().Length > 0)
                {
                    try
                    {
                        decimal val = Convert.ToDecimal(strdata.Trim());
                        if (val != decimal.MinValue)
                        {
                            return val.ToString("C");
                        }
                        else
                        {
                            return "-";
                        }
                    }
                    catch
                    {
                        return "-";
                    }
                }
                else
                    return "-";
            }


            public string FixBool(string strdata)
            {
                if (strdata.Trim().Length > 0)
                {
                    if (strdata.Trim() == "1")
                        return "ja";
                    else
                        return "nain";
                }
                else
                    return "-";
            }
            #endregion
        }
    }
     
}
