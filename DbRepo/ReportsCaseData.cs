using Application.Interfaces;
using Dapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DbRepo
{
    public class ReportsCaseData : IReportsCaseData
    {
        private string _sqlConnStr;
        public ReportsCaseData(IConfiguration config)
        {
            _sqlConnStr = config.GetConnectionString("Default");
        }

        public async Task<List<CaseModel>> CaseDetail(int appID, int? CaseTypeID, int? ContactID, int? StatusID, int? ResultID,
        DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? CaseID, int? ProfileID, string CustomerName)
        {

            var procedure = "REPORT_CaseDetail";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);
            _params.Add(name: "@CaseTypeID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseTypeID);
            _params.Add(name: "@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ContactID);
            _params.Add(name: "@StatusID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: StatusID);
            _params.Add(name: "@ResultID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ResultID);

            _params.Add(name: "@CreateDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: CreateDate1);
            _params.Add(name: "@CreateDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: CreateDate2);
            _params.Add(name: "@ModifiedDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: ModifiedDate1);
            _params.Add(name: "@ModifiedDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: ModifiedDate2);

            _params.Add(name: "@CaseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseID);
            _params.Add(name: "@ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ProfileID);
            _params.Add(name: "@CustomerName", dbType: DbType.String, direction: ParameterDirection.Input, value: CustomerName);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn .QueryAsync<CaseModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<CaseModel> _CaseModel = result.ToList<CaseModel>();

                return _CaseModel;
            }
          
        }

        public async Task<List<CaseModel>> CaseByStatus(int appID, int? CaseTypeID, int? ContactID,
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

            
                using (var conn = new SqlConnection(_sqlConnStr))
                {
                    var result = await conn .QueryAsync<CaseModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CaseModel> _CaseModel = result.ToList<CaseModel>();

                    return _CaseModel;
                }
            
        }

        public async Task<List<CaseModel>> CaseByResult(int appID, int? CaseTypeID, int? ContactID,
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

               using (var conn = new SqlConnection(_sqlConnStr))
                {
                    var result = await conn .QueryAsync<CaseModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CaseModel> _CaseModel = result.ToList<CaseModel>();

                    return _CaseModel;
                }
          
        }

        public async Task<List<CaseModel>> AliasCount(int appID, int? CaseTypeID, int? ContactID,
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

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn .QueryAsync<CaseModel>(procedure, _params, commandType: CommandType.StoredProcedure);                                      

                return result.ToList<CaseModel>();
            }
         
        }

        public async Task<List<CaseModel>> AliasDetail(int appID, int? CaseTypeID, int? ContactID, int? StatusID, int? ResultID,
            DateTime? CreateDate1, DateTime? CreateDate2, DateTime? ModifiedDate1, DateTime? ModifiedDate2, int? CaseID, int? ProfileID, string CustomerName)
        {
            var procedure = "REPORT_CaseAliasDetail";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);
            _params.Add(name: "@CaseTypeID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseTypeID);
            _params.Add(name: "@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ContactID);
            _params.Add(name: "@StatusID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: StatusID);
            _params.Add(name: "@ResultID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ResultID);

            _params.Add(name: "@CreateDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: CreateDate1);
            _params.Add(name: "@CreateDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: CreateDate2);
            _params.Add(name: "@ModifiedDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: ModifiedDate1);
            _params.Add(name: "@ModifiedDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: ModifiedDate2);

            _params.Add(name: "@CaseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseID);
            _params.Add(name: "@ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ProfileID);
            _params.Add(name: "@CustomerName", dbType: DbType.String, direction: ParameterDirection.Input, value: CustomerName);

            using (var conn = new SqlConnection(_sqlConnStr))
                {
                    var result = await conn .QueryAsync<CaseModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CaseModel> _CaseModel = result.ToList<CaseModel>();

                    return _CaseModel;
                }
           
        }
    }
}
