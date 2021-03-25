using Dapper;
using HCMDataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HCMApi
{
    [Serializable]
    [DataContract(Namespace = "DominoCMS")]
    [XmlRootAttribute("Contact")]
    public class CaseContact : ICaseContact
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


        private readonly IConfiguration _config;
        private readonly ISqlDataAccess _db;
        public CaseContact(IConfiguration config, ISqlDataAccess db)
        {
            _config = config;
            _db = db;
        }
        public CaseContact()
        {
            Email = String.Empty;
            ContactID = 0;
        }

        //public CaseContact(int vContactID)
        //{
        //    Load(vContactID, String.Empty);
        //}

        //public CaseContact(string vEmail)
        //{
        //    Load(0, vEmail);
        //}

        public async static Task<CaseContact> GetContact(int ContactID)
        {
            CaseContact contact = new CaseContact();
            await contact.Load(ContactID, String.Empty);
            return contact;
        }

        public async static Task<CaseContact> GetCreateContact(string vEmail)
        {
            CaseContact contact = new CaseContact();
            if (String.IsNullOrEmpty(await contact.Load(0, vEmail)))
                await contact.Create(vEmail);

            return contact;
        }

        public static CaseContact GetContactFromXML(string xml)
        {
            return ObjectXMLSerializer.DeserializeObject<CaseContact>(xml);
        }
        public async Task<int> Create(string vEmail)
        {
            Email = vEmail;
            ContactID = 0;
            return await Create();
        }

        public async Task<int> Create()
        {
            if (String.IsNullOrEmpty(ContactData))
                ContactData = @"<data/>";

            SqlXml xmlData = new SqlXml(new XmlTextReader(ContactData, XmlNodeType.Document, null));

            var procedure = "Contact_Create";
            var _params = new DynamicParameters();

            _params.Add(name: "@Email", dbType: DbType.String, direction: ParameterDirection.Input, value: Email);
            _params.Add(name: "@ContactURI", dbType: DbType.String, direction: ParameterDirection.Input, value: String.IsNullOrEmpty(ContactURI) ? String.Empty : ContactURI);
            _params.Add(name: "@ForeingID", dbType: DbType.String, direction: ParameterDirection.Input, value: String.IsNullOrEmpty(ForeingID) ? String.Empty : ForeingID);
            _params.Add(name: "@Name", dbType: DbType.String, direction: ParameterDirection.Input, value: String.IsNullOrEmpty(Name) ? String.Empty : Name);
            _params.Add(name: "@NickName", dbType: DbType.String, direction: ParameterDirection.Input, value: String.IsNullOrEmpty(NickName) ? String.Empty : NickName);
            _params.Add(name: "@Function", dbType: DbType.String, direction: ParameterDirection.Input, value: String.IsNullOrEmpty(Function) ? String.Empty : Function);
            _params.Add(name: "@ContactData", dbType: DbType.Xml, direction: ParameterDirection.Input, value: ContactData);

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {

                    var result = await conn.QueryAsync<CaseContact>(procedure, _params, commandType: CommandType.StoredProcedure);
                    //int ReturnValue = _params.Get<int>("@ReturnValue");

                    //if (ReturnValue != 0)
                    //{
                    //    throw new Exception("Create.UndefinedError");
                    //}

                    Email = result.ToString();
                    return 1;

                }
            }
            catch
            {
                return -1;
            }
            

        }

        public string AsXml()
        {
            XmlDocument xDoc = ObjectXMLSerializer.SerializeObjectAsXmlDoc<CaseContact>(this);
            //string s =ObjectXMLSerializer.SerializeObject<CaseContact>(this);
            //xDoc.LoadXml(s);
            return xDoc.DocumentElement.OuterXml;

        }

        private async Task<string> Load(int vContactID, string vEmail)
        {
            Email = String.Empty;

            var procedure = "Contact_GetOne";
            var _params = new DynamicParameters();

            _params.Add(name: "@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: vContactID);
            _params.Add(name: "@Email", dbType: DbType.String, direction: ParameterDirection.Input, value: vEmail);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {
                    var result = await conn.QueryAsync<CaseContact>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CaseContact> _CaseContact = result.ToList<CaseContact>();
                    if (_CaseContact.Count > 0)
                    {
                        Email = _CaseContact[0].Email;
                        ContactID = _CaseContact[0].ContactID;
                        ContactURI = _CaseContact[0].ContactURI;
                        ForeingID = _CaseContact[0].ForeingID;
                        Name = _CaseContact[0].Name;
                        NickName = _CaseContact[0].NickName;
                        Function = _CaseContact[0].Function;

                        return Email;
                    }

                    return null;
                }
            }
            catch
            {
                return null;
            }

            
        }
        public async Task<List<CaseContact>> Contact_GetAll()
        {
            var procedure = "Contact_GetAll";
            var _params = new DynamicParameters();

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {
                    var result = await conn.QueryAsync<CaseContact>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CaseContact> _CaseContact = result.ToList<CaseContact>();                   

                    return _CaseContact;
                }
            }
            catch
            {
                return null;
            }

            
        }
    }

    /// <summary>
    /// class for storing collection of  CaseContact items
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "DominoCMS")]
    public class CaseContactList : Hashtable
    {
        public CaseContactList() { }

        protected CaseContactList(SerializationInfo info, StreamingContext context) : base(info, context) 
        { 
        }

        public void CopyTo(Exception[] array, int index)
        {
            base.CopyTo(array, index);
        }
        public override bool Contains(Object key)
        {
            return base.Contains(key.ToString().ToUpper());
        }

        public static CaseContactList LoadFromXml(string xml)
        {
            CaseContactList _participants = new CaseContactList();
            if (!String.IsNullOrEmpty(xml))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xml);
                foreach (XmlNode nContact in xDoc.SelectNodes(@"//Contact"))
                {
                    //CaseContact c = CaseContact.GetContact(Convert.ToInt32(nContact.Attributes["ContactID"].Value));
                    CaseContact c = CaseContact.GetContactFromXML(nContact.OuterXml);
                    _participants.Add(c.ContactID, c);
                }
            }
            return _participants;
        }
        public static List<CaseContact> LoadFromXmlAsList(string xml)
        {
            List<CaseContact> _CaseContact = new List<CaseContact>();
            if (!String.IsNullOrEmpty(xml))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xml);
                foreach (XmlNode nContact in xDoc.SelectNodes(@"//Contact"))
                {
                    //CaseContact c = CaseContact.GetContact(Convert.ToInt32(nContact.Attributes["ContactID"].Value));
                    CaseContact c = CaseContact.GetContactFromXML(nContact.OuterXml);
                    _CaseContact.Add(c);
                }
            }
            return _CaseContact;
        }





        //public static CaseContactList LoadFromIDs(string ids)
        //{
        //    CaseContactList _contacts = new CaseContactList();
        //    if (!String.IsNullOrEmpty(ids))
        //    {
        //        SqlParameter[] parms = null;
        //        string[] _ids = ids.Split(',');
        //        DataTable dt = DAO.ExecSqlProcedureReturnDt("Contact_GetAll", parms);
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            bool found = false;
        //            int _id = -1;
        //            for (int i = 0; i < _ids.Length; i++)
        //            {
        //                int.TryParse(_ids[i], out _id);
        //                if (_id == Convert.ToInt32(dr["ContactID"]))
        //                {
        //                    found = true;
        //                    break;
        //                }
        //            }
        //            if (found)
        //            {
        //                CaseContact c = new CaseContact();
        //                c.ContactID = Convert.ToInt32(dr["ContactID"]);
        //                c.ContactData = dr["ContactData"].ToString();
        //                c.ContactURI = dr["ContactURI"].ToString();
        //                c.Email = dr["Email"].ToString();
        //                c.ForeingID = dr["ForeingID"].ToString();
        //                c.Name = dr["Name"].ToString();
        //                c.NickName = dr["NickName"].ToString();
        //                c.Function = dr["Function"].ToString();


        //                _contacts.Add(c.ContactID, c);
        //            }

        //        }
        //    }
        //    return _contacts;
        //}
        //public static CaseContactList GetSuggestionsList(string Keyword)
        //{
        //    CaseContactList _contacts = new CaseContactList();

        //    SqlParameter[] parms = new SqlParameter[] {
        //                   new SqlParameter("@Keyword", SqlDbType.VarChar, 50)
        //    };
        //    parms[0].Value = Keyword;

        //    DataTable dt = DAO.ExecSqlProcedureReturnDt("Contact_GetSuggestions", parms);
        //    foreach (DataRow dr in dt.Rows)
        //    {

        //        CaseContact c = new CaseContact();
        //        c.ContactID = Convert.ToInt32(dr["ContactID"]);
        //        c.ContactData = dr["ContactData"].ToString();
        //        c.ContactURI = dr["ContactURI"].ToString();
        //        c.Email = dr["Email"].ToString();
        //        c.ForeingID = dr["ForeingID"].ToString();
        //        c.Name = dr["Name"].ToString();
        //        c.NickName = dr["NickName"].ToString();
        //        c.Function = dr["Function"].ToString();

        //        _contacts.Add(c.ContactID, c);

        //    }


        //    return _contacts;
        //}
        //public XmlDocument AsXml()
        //{
        //    XmlDocument xDoc = new XmlDocument();
        //    xDoc.LoadXml(AsXmlString());
        //    return xDoc;
        //}

        public string AsXmlString()
        {
            XmlDocument xDoc = new XmlDocument();
            string s = @"<ContactList>";
            foreach (var key in base.Keys)
            {
                s += ((CaseContact)base[key]).AsXml();
            }
            s += @"</ContactList>";
            return s;
        }
    }
}
