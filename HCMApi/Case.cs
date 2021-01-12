using Dapper;
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
    public class Case : ICase
    {
        public int CaseID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastActivity { get; set; }
        public int IssuedByID { get; set; }
        public int? CaseStatusID { get; set; }
        public string CaseStatus { get; set; }
        public int CaseResultID { get; set; }
        public string CaseResult { get; set; }
        public string CaseType { get; set; }
        public int? CaseTypeID { get; set; }
        public int ProfileID { get; set; }
        public string ProfileName { get; set; }
        public string Subject { get; set; }
        public int? appID { get; set; }
        public string appName { get; set; }
        public string ContactName { get; set; }
        public string CaseSource { get; set; }
        public int ModifiedByID { get; set; }
        public string ModifiedByName { get; set; }
        public int MinDuration { get; set; }
        //public CaseContactList Participants { get; set; }
        public XmlDocument CaseData { get; set; }
        public string CustomerName { get; set; }
        public string SapUser { get; set; }

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
        //    Load(_caseID);
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


            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = await conn.QueryAsync<Case>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<Case> _CaseList = result.ToList<Case>();
                    return _CaseList;
                }
            }
            catch (Exception ex)
            {
                return null;
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
    }
}
