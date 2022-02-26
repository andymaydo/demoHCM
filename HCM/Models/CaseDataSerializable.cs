using System;
using System.Xml.Serialization;
using static HCMModels.CaseModel;

namespace HCM.Models
{

    [Serializable()]
    [XmlRoot("Root")]
    public class CaseBatchData
    {
        
        public CaseBatch Batch { get; set; }

    }

    [Serializable()]
    [XmlRoot("Root")]
    public class CaseTranData
    {
        
        public CaseTran Tran { get; set; }

    }



    [Serializable()]
    [XmlType(AnonymousType = true)]
    public class CaseBatch
    {
        public CaseSettings Settings { get; set; }
        public CaseSrc Src { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("Res")]
        public CaseRes[] Res { get; set; }
    }

    [Serializable()]
    [XmlType(AnonymousType = true)]
    public class CaseTran
    {
        public CaseSettings Settings { get; set; }
        public CaseSrc Src { get; set; }

        [XmlElement("Res")]
        public CaseRes[] Res { get; set; }

        public CaseOriginator BENUTZER { get; set; }
    }


    [Serializable()]
    [XmlType(AnonymousType = true)]
    public class CaseSettings
    {
        public int profileid { get; set; }
        public string profilename { get; set; }
        public string sapip { get; set; }
        public string sapgw { get; set; }
        public string sapmandant { get; set; }
        public string saprfcdest { get; set; }
        public int batchid { get; set; }
        public int tranid { get; set; }
        public System.DateTime startedon { get; set; }
        public System.DateTime finishedon { get; set; }
        public string denlists { get; set; }
        public int verid { get; set; }
        public string checktype { get; set; }
        public string matchtype { get; set; }
        public int redalert { get; set; }
        public int totalrecords { get; set; }
        public int matchedrecord { get; set; }
        public int totalmatches { get; set; }
        public int maxmatchprcnt { get; set; }
        public string sapbk { get; set; }
        public string sapvo { get; set; }
        public string sapwk { get; set; }
        public string saptrn { get; set; }
        public string sapusr { get; set; }
        public string sapvar { get; set; }
        public int matchtypeid { get; set; }
        public int checktypeid { get; set; }
        public byte startverid { get; set; }
}


    [Serializable()]
    [XmlType(AnonymousType = true)]
    public class CaseSrc
    {
        public string clid { get; set; }
        public string sourceid { get; set; }
        public string name { get; set; }
        public string street { get; set; } 
        public string city { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string vbeln { get; set; }
        public int tranid { get; set; }
        public int index { get; set; }
        public int res { get; set; }
    }


    [Serializable()]
    [XmlType(AnonymousType = true)]
    public class CaseRes
    {
        public int res { get; set; }
        public string clid { get; set; }
        public int denid { get; set; }
        public string name { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string notes { get; set; }
        public string dentype { get; set; }
        public int denytypeid { get; set; }
        public string sourceurl { get; set; }
        public System.DateTime date { get; set; }
        public int denlistsver { get; set; }
       
        public int tranid { get; set; }
        public byte matchlevel { get; set; }
        public int index { get; set; }
    }
    
}
