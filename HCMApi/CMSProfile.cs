using Dapper;
using HCMApi.Models;
using HCMDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HCMApi
{
    [Serializable]
    //[DataContract(Namespace = "DominoCMS")]
    public class CMSProfile : ICMSProfile
    {
        public int profileID { get; set; }
        public int appID { get; set; }
        public string appName { get; set; }
        public int profileStatusID { get; set; }
        public string profileStatus { get; set; }
        public int profileType { get; set; }

        public string profileName { get; set; }
        public string profilDescr { get; set; }
        public string EmailLanguage { get; set; }

        public CaseContactList profileParticipants { get; set; }
        public CaseContactList EscalationUsers { get; set; }
        public CaseRuleList EscalationRules { get; set; }
        public bool NotifyAllProfileParticipants { get; set; }

        public string profileURI { get; set; }
        public string WebURL { get; set; }
        public string WebAuthenitcationType { get; set; }
        public int ModifiedBy { get; set; }
        public string ReasonToDelete { get; set; }

        private readonly ISqlDataAccess _db;

        public CMSProfile()
        {
        }

        public CMSProfile(ISqlDataAccess db)
        {
            _db = db;
            NotifyAllProfileParticipants = false;
        }

        public  CMSProfileModel Load(int _profileID)
        {

            CMSProfileModel _CMSProfileModel = new CMSProfileModel();

            var procedure = "Profile_GetOne";
            var _params = new DynamicParameters();

            _params.Add(name: "@ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: _profileID);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {
                    var result =  conn.Query<CMSProfileModelSimple>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CMSProfileModelSimple> _CMSProfileModelSimple = result.ToList<CMSProfileModelSimple>();
                    if (_CMSProfileModelSimple != null && _CMSProfileModelSimple.Count > 0)
                    {

                        PropertyInfo[] destinationProperties = _CMSProfileModel.GetType().GetProperties();
                        foreach (PropertyInfo destinationPI in destinationProperties)
                        {
                            if ( destinationPI.Name == "profileParticipants")
                            {
                                string _profileParticipants = _CMSProfileModelSimple[0].profileParticipants;
                                try { 
                                    if (!String.IsNullOrEmpty(_profileParticipants))
                                    {
                                        _CMSProfileModel.profileParticipants= CaseContactList.LoadFromXmlAsList(_profileParticipants);
                                    }
                                }
                                catch { };
                            }
                            else if ( destinationPI.Name == "escalationUsers")
                            {
                                string _escalationUsers = _CMSProfileModelSimple[0].escalationUsers;
                                try { 
                                    if (!String.IsNullOrEmpty(_escalationUsers))
                                    {
                                        _CMSProfileModel.escalationUsers = CaseContactList.LoadFromXmlAsList(_escalationUsers);
                                    }
                                }
                                catch { };
                            }
                            else if ( destinationPI.Name == "escalationRules")
                            {
                                string _escalationRules = _CMSProfileModelSimple[0].escalationRules;
                                try { 
                                    if (!String.IsNullOrEmpty(_escalationRules))
                                    {
                                        _CMSProfileModel.escalationRules = CaseRuleList.LoadFromXmlAsList(_escalationRules);
                                    }
                                }
                                catch { };
                            }
                            else 
                            { 
                                PropertyInfo sourcePI = _CMSProfileModelSimple[0].GetType().GetProperty(destinationPI.Name);

                                destinationPI.SetValue(_CMSProfileModel,
                                                       sourcePI.GetValue(_CMSProfileModelSimple[0], null), 
                                                       null);
                            }
                        }
                        

                        return _CMSProfileModel;
                    }

                    return null;
                }
            }
            catch //(Exception ex)
            {
                return null;
            }


        }

        //private void Load(int _profileID)
        //{
        //    profileParticipants = new CaseContactList();
        //    EscalationUsers = new CaseContactList();
        //    EscalationRules = new CaseRuleList();

        //    SqlParameter[] parms = new SqlParameter[] {
        //                   new SqlParameter("@ProfileID", SqlDbType.Int)
        //    };
        //    parms[0].Value = _profileID;

        //    DataTable dt = DAO.ExecSqlProcedureReturnDt("Profile_GetOne", parms);
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        ProfileID = _profileID;
        //        appID = Convert.ToInt32(dr["appID"]);
        //        appName = dr["appName"].ToString();
        //        profileStatusID = Convert.ToInt32(dr["profileStatusID"]);
        //        //profileStatus = dr["ProfileStatus"].ToString();
        //        profileType = Convert.ToInt32(dr["profileType"]);

        //        profileName = dr["profileName"].ToString();
        //        profilDescr = dr["profilDescr"].ToString();
        //        NotifyAllProfileParticipants = Convert.ToBoolean(dr["NotifyAllProfileParticipants"]);

        //        if (!string.IsNullOrEmpty(dr["profileParticipants"].ToString()))
        //            profileParticipants = CaseContactList.LoadFromXml(dr["profileParticipants"].ToString());

        //        if (!string.IsNullOrEmpty(dr["EscalationUsers"].ToString()))
        //            EscalationUsers = CaseContactList.LoadFromXml(dr["EscalationUsers"].ToString());

        //        if (!string.IsNullOrEmpty(dr["EscalationRules"].ToString()))
        //            EscalationRules = CaseRuleList.LoadFromXml(dr["EscalationRules"].ToString());


        //        profileURI = dr["profileURI"].ToString();
        //        WebURL = dr["WebURL"].ToString();
        //        WebAuthenitcationType = dr["WebAuthenitcationType"].ToString();

        //        if (!(dr["ModifiedBy"] is System.DBNull))
        //        {
        //            ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]);
        //        }

        //        ReasonToDelete = dr["ReasonToDelete"].ToString();
        //        EmailLanguage = dr["NotificationLang"].ToString();

        //    }
        //}

        #region Save
        public async Task<bool> UpdateParticipantsAsync(CMSProfileModel cmsProfileModel)
        {
            var procedure = "Profile_Update_Participants";
            var _params = new DynamicParameters();

            _params.Add(name: "@ProfilID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.profileID);
            _params.Add(name: "@ModifiedBy", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.ModifiedBy);
            _params.Add(name: "@NotifyAllProfileParticipants", dbType: DbType.Boolean, direction: ParameterDirection.Input, value: cmsProfileModel.NotifyAllProfileParticipants);
            _params.Add(name: "@profileParticipants", dbType: DbType.Xml, direction: ParameterDirection.Input, value: cmsProfileModel.ProfileParticipants_AsXmlString());
            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {

                    var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);
                    int ReturnValue = _params.Get<int>("@ReturnValue");

                    if (ReturnValue != 0)
                    {
                        throw new Exception("UpdateParticipantsAsync.UndefinedError");
                    }

                    return true;

                }
            }
            catch //( Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> UpdateEscalationAsync(CMSProfileModel cmsProfileModel)
        {
            var procedure = "Profile_Update_Escalation";
            var _params = new DynamicParameters();

            _params.Add(name: "@ProfilID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.profileID);
            _params.Add(name: "@ModifiedBy", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.ModifiedBy);
            _params.Add(name: "@EscalationUsers", dbType: DbType.Xml, direction: ParameterDirection.Input, value: cmsProfileModel.EscalationUsers_AsXmlString());
            _params.Add(name: "@EscalationRules", dbType: DbType.Xml, direction: ParameterDirection.Input, value: cmsProfileModel.EscalationRules_AsXmlString());
            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {

                    var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);
                    int ReturnValue = _params.Get<int>("@ReturnValue");

                    if (ReturnValue != 0)
                    {
                        throw new Exception("UpdateParticipantsAsync.UndefinedError");
                    }

                    return true;

                }
            }
            catch //( Exception ex)
            {
                return false;
            }
            
        }
        public async Task<bool> UpdateGeneralInfoAsync(CMSProfileModel cmsProfileModel)
        {
            var procedure = "Profile_Update_General";
            var _params = new DynamicParameters();

            _params.Add(name: "@ProfilID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.profileID);
            _params.Add(name: "@ModifiedBy", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.ModifiedBy);
            _params.Add(name: "@ProfileName", dbType: DbType.String, direction: ParameterDirection.Input, value: cmsProfileModel.profileName);
            _params.Add(name: "@ProfilDescr", dbType: DbType.String, direction: ParameterDirection.Input, value: cmsProfileModel.profilDescr);
            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.appID);
            _params.Add(name: "@NotificationLang", dbType: DbType.String, direction: ParameterDirection.Input, value: cmsProfileModel.NotificationLang);
            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

           
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {

                    var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);                    
                    return true;

                }
           

            
        }
        public async Task<bool> UpdateStatusAsync(CMSProfileModel cmsProfileModel)
        {
            var procedure = "Profile_Update_Status";
            var _params = new DynamicParameters();

            _params.Add(name: "@ProfilID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.profileID);
            _params.Add(name: "@ModifiedBy", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.ModifiedBy);
            _params.Add(name: "@profileStatusID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: cmsProfileModel.profileStatusID);
            _params.Add(name: "@ReasonToDelete", dbType: DbType.String, direction: ParameterDirection.Input, value: cmsProfileModel.ReasonToDelete);
            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {

                    var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);
                    int ReturnValue = _params.Get<int>("@ReturnValue");

                    if (ReturnValue != 0)
                    {
                        throw new Exception("UpdateStatusAsync.UndefinedError");
                    }

                    return true;

                }
            }
            catch //( Exception ex)
            {
                return false;
            }

        }
        #endregion




        public async Task<List<CMSProfileModel>> GetList(int? AppID, int? StatusID)
        {
            var procedure = "Profile_GetList";
            var _params = new DynamicParameters();

            if (AppID == null)
            {
                _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: System.DBNull.Value);
            }
            else
            {
                _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: AppID);
            }
            
            if (StatusID == null )
            {
                _params.Add(name: "@profileStatusID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: System.DBNull.Value);
            }
            else
            {
                _params.Add(name: "@profileStatusID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: StatusID);
            }
            
            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

           
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {
                    var result = await conn.QueryAsync<CMSProfileModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CMSProfileModel> _CMSProfile = result.ToList<CMSProfileModel>();

                    return _CMSProfile;
                }
            

        }

        public async Task<List<CMSProfileModel>> GetDeleteList(int? AppID)
        {
            var procedure = "Profile_GetDeleteList";
            var _params = new DynamicParameters();

            if (AppID == null)
            {
                _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: System.DBNull.Value);
            }
            else
            {
                _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: AppID);
            }
            
            
            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

           
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {
                    var result = await conn.QueryAsync<CMSProfileModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CMSProfileModel> _CMSProfile = result.ToList<CMSProfileModel>();

                    return _CMSProfile;
                }
            

        }

        //public static DataTable GetDeleteList(int? AppID)
        //{
        //    SqlParameter[] parms = new SqlParameter[] {
        //                   new SqlParameter("@appID", SqlDbType.Int)
        //    };
        //    if (AppID != null)
        //        parms[0].Value = AppID;
        //    else
        //        parms[0].Value = DBNull.Value;

        //    return DAO.ExecSqlProcedureReturnDt("Profile_GetDeleteList", parms);

        //}

        public async Task<CMSProfileModel> CreateNew(int ModifiedBy, int appID, string profileName, string profilDescr, string EmailLanguage)
        {
            var procedure = "Profile_Create";
            var _params = new DynamicParameters();

            _params.Add(name: "@ModifiedBy", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ModifiedBy);
            _params.Add(name: "@ProfileName", dbType: DbType.String, direction: ParameterDirection.Input, value: profileName);
            _params.Add(name: "@ProfilDescr", dbType: DbType.String, direction: ParameterDirection.Input, value: profilDescr);
            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);
            _params.Add(name: "@NotificationLang", dbType: DbType.String, direction: ParameterDirection.Input, value: EmailLanguage);
            _params.Add(name: "@ProfilID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {

                    var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);
                    int ReturnValue = _params.Get<int>("@ReturnValue");

                    if (ReturnValue == 0)
                    {
                        return Load(_params.Get<Int32>("@ProfilID"));
                    }

                    return null;

                }
            



        }

        public async Task<CMSProfileModel> ProfilCopy(int SourceProfilID, int ModifiedBy)
        {

            var procedure = "Profile_Copy";
            var _params = new DynamicParameters();

            _params.Add(name: "@SourceProfilID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: SourceProfilID);
            _params.Add(name: "@ModifiedBy", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ModifiedBy);
            _params.Add(name: "@ProfilID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {

                    var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);
                    int ReturnValue = _params.Get<int>("@ReturnValue");

                    if (ReturnValue == 0)
                    {
                        return  Load(_params.Get<Int32>("@ProfilID"));
                    }

                    return null;

                }
            

        }

    }
}
