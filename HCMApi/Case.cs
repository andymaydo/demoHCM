using Dapper;
using HCMModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace HCMApi
{
    [Serializable]
    [DataContract(Namespace = "DominoCMS")]
    public class Case : CaseModel, ICase
    {       

        public CaseContactList ParticipantsAsCaseContactList 
        { 
            get
            {
                return CaseContactList.LoadFromXml(Participants);
            }
        }
        public List<CaseContact> ParticipantsAsList 
        { 
            get
            {
                return CaseContactList.LoadFromXmlAsList(Participants);
            }
        }
        

        private readonly IConfiguration _config;

        public Case(IConfiguration config)
        {
            _config = config;
        }

        public Case()
        {
            CaseID = 0;
        }

        //public Case(int _caseID)
        //{
        //    await Load(_caseID);
        //}

        public async Task<List<Case>> GetCasesList(int? filterCaseID, int? appID, int? caseTypeID, int? caseStatusID, int loginUserID,
            DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? ProfileID, string CustomerName)
        {

            var procedure = "Case_GetList";
            var _params = new DynamicParameters();

            _params.Add(name: "@caseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: filterCaseID);
            _params.Add(name: "@AppID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);
            _params.Add(name: "@CaseTypeID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseTypeID);
            _params.Add(name: "@CaseStatusID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseStatusID);
            _params.Add(name: "@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: loginUserID);

            _params.Add(name: "@CreateDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: CreateDate1);
            _params.Add(name: "@CreateDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: CreateDate2);
            _params.Add(name: "@ModifiedDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: ModifiedDate1);
            _params.Add(name: "@ModifiedDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: ModifiedDate2);

            _params.Add(name: "@ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ProfileID);
            _params.Add(name: "@CustomerName", dbType: DbType.String, direction: ParameterDirection.Input, value: CustomerName);


            
            using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
            {
                var result = await conn.QueryAsync<Case>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<Case> _CaseList = result.ToList<Case>();
                return _CaseList;
            }
            


            //SqlParameter[] parms = new SqlParameter[] {
            //               new SqlParameter("@caseID", SqlDbType.Int),
            //               new SqlParameter("@AppID", SqlDbType.Int),
            //               new SqlParameter("@CaseTypeID", SqlDbType.Int),
            //               new SqlParameter("@CaseStatusID", SqlDbType.Int),
            //               new SqlParameter("@ContactID", SqlDbType.Int),
            //               new SqlParameter("@CreateDate1", SqlDbType.DateTime),
            //               new SqlParameter("@CreateDate2", SqlDbType.DateTime),
            //               new SqlParameter("@ModifiedDate1", SqlDbType.DateTime),
            //               new SqlParameter("@ModifiedDate2", SqlDbType.DateTime),
            //               new SqlParameter("@ProfileID", SqlDbType.Int),
            //            new SqlParameter("@CustomerName", SqlDbType.NVarChar, 500)
            //};
            //parms[0].Value = filterCaseID;
            //parms[1].Value = appID;
            //parms[2].Value = caseTypeID;
            //parms[3].Value = caseStatusID;
            //parms[4].Value = loginUserID;
            //parms[5].Value = CreateDate1;
            //parms[6].Value = CreateDate2;
            //parms[7].Value = ModifiedDate1;
            //parms[8].Value = ModifiedDate2;
            //parms[9].Value = ProfileID;
            //parms[10].Value = CustomerName;

            //return DAO.ExecSqlProcedureReturnDt("Case_GetList", parms);
        }

        public async Task<Case> Load(int _caseID)
        {

            var procedure = "Case_GetOne";
            var _params = new DynamicParameters();

            _params.Add(name: "@caseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: _caseID);

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = await conn.QueryAsync<Case>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<Case> _CaseList = result.ToList<Case>();
                    if (_CaseList.Count > 0)
                    {
                        Case _Case = new Case();
                        _Case = _CaseList[0];

                        //_Case.ParticipantsAsXml = CaseContactList.LoadFromXml(_CaseList[0].Participants);

                        //if ( ! string.IsNullOrEmpty(_CaseList[0].CaseData) )
                        //{
                        //    _Case.CaseDataAsXml = new XmlDocument();
                        //    _Case.CaseDataAsXml.LoadXml(_CaseList[0].CaseData);
                        //}
                        

                        return _Case;
                    }
                    return null;
                }
            }
            catch //(Exception ex)
            {
                return null;
            }



            //SqlParameter[] parms = new SqlParameter[] {
            //               new SqlParameter("@caseID", SqlDbType.Int)
            //};
            //parms[0].Value = _caseID;

            //DataTable dt = DAO.ExecSqlProcedureReturnDt("Case_GetOne", parms);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    CaseID          = _caseID;
            //    IssuedByID      = Convert.ToInt32(dr["IssuedID"]);
            //    CreateDate      = Convert.ToDateTime(dr["CreateDate"]);
            //    LastActivity    = Convert.ToDateTime(dr["LastActivity"]);
            //    CaseStatusID    = Convert.ToInt32(dr["CaseStatusID"]);
            //    CaseStatus      = dr["CaseStatus"].ToString();
            //    CaseResultID    = Convert.ToInt32(dr["CaseResultID"]);
            //    CaseResult      = dr["CaseResult"].ToString();
            //    CaseTypeID      = Convert.ToInt32(dr["CaseTypeID"]);
            //    CaseType        = dr["CaseType"].ToString();
            //    appID           = Convert.ToInt32(dr["appID"]);
            //    appName         = dr["appName"].ToString();
            //    ProfileID       = Convert.ToInt32(dr["ProfileID"]);
            //    ProfileName     = dr["profileName"].ToString();
            //    Participants    = CaseContactList.LoadFromXml(dr["Participants"].ToString());
            //    ContactName     = dr["ContactName"].ToString();
            //    CaseSource      = dr["CaseSource"].ToString();
            //    CustomerName    = dr["CustomerName"].ToString();

            //    //ModifiedByID    = Convert.ToInt32(dr["ModifiedByID"]);
            //    ModifiedByName = dr["ModifiedByName"].ToString();
            //    SapUser = dr["SapUser"].ToString();

            //    MinDuration = Convert.ToInt32(dr["Duration"]);

            //    CaseData = null;
            //    if ( ! string.IsNullOrEmpty(dr["CaseData"].ToString()) )
            //    {
            //        CaseData = new XmlDocument();
            //        CaseData.LoadXml(dr["CaseData"].ToString());
            //    }

            //    Subject = dr["Subject"].ToString();
            //}
        }

        public async Task<int> Create()
        {

            var procedure = "Case_Create";
            var _params = new DynamicParameters();

            _params.Add(name: "@IssuedID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: IssuedID);
            _params.Add(name: "@CaseStatusID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseStatusID);
            _params.Add(name: "@CaseResultID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseResultID);
            _params.Add(name: "@CaseTypeID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseTypeID);
            _params.Add(name: "@ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ProfileID);
            _params.Add(name: "@CaseData", dbType: DbType.Xml, direction: ParameterDirection.Input, value: CaseDataAsXml);
            _params.Add(name: "@Participants", dbType: DbType.Xml, direction: ParameterDirection.Input, value: ParticipantsAsXml);
            _params.Add(name: "@Subject", dbType: DbType.String, direction: ParameterDirection.Input, value: Subject);
            _params.Add(name: "@CaseSource", dbType: DbType.String, direction: ParameterDirection.Input, value: CaseSource);

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = await conn.QueryAsync<Case>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<Case> _CaseList = result.ToList<Case>();
                    if (_CaseList.Count > 0)
                    {
                        return _CaseList[0].CaseID;
                    }
                    return -1;
                }
            }
            catch //(Exception ex)
            {
                return -1;
            }



            
        }

        public int UpdateParticipants(int _caseID, string _participants)
        {

            var procedure = "Case_UpdateParticipants";
            var _params = new DynamicParameters();

            _params.Add(name: "@caseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: _caseID);
            _params.Add(name: "@Participants", dbType: DbType.Xml, direction: ParameterDirection.Input, value: _participants);

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    conn.Query(procedure, _params, commandType: CommandType.StoredProcedure);
                    return 1;
                }
            }
            catch //(Exception ex)
            {
                return -1;
            }



            
        }





        public async Task<List<CaseModel.CaseEventStatus>> GetStatusByCase(int caseID)
        {

            var procedure = "Case_GetEventByStatus";
            var _params = new DynamicParameters();

            _params.Add(name: "@caseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseID);
            

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = await conn.QueryAsync<CaseModel.CaseEventStatus>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CaseModel.CaseEventStatus> _CaseStatusList = result.ToList<CaseModel.CaseEventStatus>();
                    return _CaseStatusList;
                }
            }
            catch //(Exception ex)
            {
                return null;
            }


            
        }
    }
}
