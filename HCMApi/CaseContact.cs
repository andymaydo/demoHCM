using Dapper;
using Microsoft.Extensions.Configuration;
using System;
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

        public CaseContact(IConfiguration config)
        {
            _config = config;
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
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
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

            //SqlParameter[] parms = new SqlParameter[] {
            //new SqlParameter("@Email", SqlDbType.NVarChar),
            //new SqlParameter("@ContactURI", SqlDbType.NVarChar),
            //new SqlParameter("@ForeingID", SqlDbType.NVarChar),
            //new SqlParameter("@Name", SqlDbType.NVarChar),
            //new SqlParameter("@NickName", SqlDbType.NVarChar),
            //new SqlParameter("@Function", SqlDbType.NVarChar),
            //new SqlParameter("@ContactData", SqlDbType.Xml)
            //};
            //parms[0].Value = Email;
            //parms[1].Value = String.IsNullOrEmpty(ContactURI) ? String.Empty : ContactURI;
            //parms[2].Value = String.IsNullOrEmpty(ForeingID) ? String.Empty : ForeingID;
            //parms[3].Value = String.IsNullOrEmpty(Name) ? String.Empty : Name;
            //parms[4].Value = String.IsNullOrEmpty(NickName) ? String.Empty : NickName;
            //parms[5].Value = String.IsNullOrEmpty(Function) ? String.Empty : Function;
            //parms[6].Value = ContactData;

            //object o = DAO.ExecuteScalar(CommandType.StoredProcedure, "Contact_Create", parms);
            //Email = o.ToString();

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
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
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

            //Email = String.Empty;
            //SqlParameter[] parms = new SqlParameter[] {
            //               new SqlParameter("@ContactID", SqlDbType.Int),
            //               new SqlParameter("@Email", SqlDbType.NVarChar)
            //};
            //parms[0].Value = vContactID;
            //parms[1].Value = vEmail;

            //DataTable dt = DAO.ExecSqlProcedureReturnDt("Contact_GetOne", parms);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    Email = dr["Email"].ToString();
            //    ContactID = Convert.ToInt32(dr["ContactID"]);
            //    ContactURI = dr["ContactURI"].ToString();
            //    ForeingID = dr["ForeingID"].ToString();
            //    Name = dr["Name"].ToString();
            //    ForeingID = dr["ForeingID"].ToString();
            //    NickName = dr["NickName"].ToString();
            //    Function = dr["Function"].ToString();
            //    // ContactData = dr["ContactData"].ToString();
            //}
            //return Email;
        }
    }
}
