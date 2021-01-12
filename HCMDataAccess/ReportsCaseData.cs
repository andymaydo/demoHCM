using Dapper;
using HCMDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HCMDataAccess.Models.ReportsModels;

namespace HCMDataAccess
{
    public class ReportsCaseData : IReportsCaseData
    {
        private readonly ISqlDataAccess _db;
        public ReportsCaseData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<ReportCaseByStatusModels>> CaseByStatus(int appID, int? CaseTypeID, int? ContactID,
            DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? CaseID, int? ProfileID, string CustomerName)
        {
            var procedure = "REPORT_CaseByStatus";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);
            _params.Add(name: "@CaseTypeID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseTypeID);
            _params.Add(name: "@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ContactID);

            _params.Add(name: "@CreateDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: CreateDate1);
            _params.Add(name: "@CreateDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: CreateDate2);
            _params.Add(name: "@ModifiedDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: ModifiedDate1);
            _params.Add(name: "@ModifiedDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: ModifiedDate2);

            _params.Add(name: "@CaseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseID);
            _params.Add(name: "@ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ProfileID);
            _params.Add(name: "@CustomerName", dbType: DbType.String, direction: ParameterDirection.Input, value: CustomerName);

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {
                    var result = conn.Query<ReportCaseByStatusModels>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<ReportCaseByStatusModels> _ReportCaseByStatus = result.ToList<ReportCaseByStatusModels>();

                    return _ReportCaseByStatus;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ReportCaseByResultModels>> CaseByResult(int appID, int? CaseTypeID, int? ContactID,
            DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? CaseID, int? ProfileID, string CustomerName)
        {
            var procedure = "REPORT_CaseByResult";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);
            _params.Add(name: "@CaseTypeID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseTypeID);
            _params.Add(name: "@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ContactID);

            _params.Add(name: "@CreateDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: CreateDate1);
            _params.Add(name: "@CreateDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: CreateDate2);
            _params.Add(name: "@ModifiedDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: ModifiedDate1);
            _params.Add(name: "@ModifiedDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: ModifiedDate2);

            _params.Add(name: "@CaseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseID);
            _params.Add(name: "@ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ProfileID);
            _params.Add(name: "@CustomerName", dbType: DbType.String, direction: ParameterDirection.Input, value: CustomerName);

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {
                    var result = conn.Query<ReportCaseByResultModels>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<ReportCaseByResultModels> _ReportCaseByResult = result.ToList<ReportCaseByResultModels>();

                    return _ReportCaseByResult;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ReportAliasCountModels>> AliasCount(int appID, int? CaseTypeID, int? ContactID,
            DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? CaseID, int? ProfileID, string CustomerName)
        {
            var procedure = "REPORT_CaseByAlias";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);
            _params.Add(name: "@CaseTypeID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseTypeID);
            _params.Add(name: "@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ContactID);

            _params.Add(name: "@CreateDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: CreateDate1);
            _params.Add(name: "@CreateDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: CreateDate2);
            _params.Add(name: "@ModifiedDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: ModifiedDate1);
            _params.Add(name: "@ModifiedDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: ModifiedDate2);

            _params.Add(name: "@CaseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseID);
            _params.Add(name: "@ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ProfileID);
            _params.Add(name: "@CustomerName", dbType: DbType.String, direction: ParameterDirection.Input, value: CustomerName);

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {
                    var result = conn.Query<ReportAliasCountModels>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<ReportAliasCountModels> _ReportAliasCount = result.ToList<ReportAliasCountModels>();

                    return _ReportAliasCount;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
