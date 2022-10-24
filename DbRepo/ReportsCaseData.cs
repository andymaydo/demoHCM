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

    
        public async Task<List<CaseModel>> CaseDetail(CaseFilterBase caseFilter, int? ContactID)
        {

            var procedure = "REPORT_CaseDetail";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.GateId);
            _params.Add(name: "@CaseTypeID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.CategoryId);
            _params.Add(name: "@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ContactID);
            _params.Add(name: "@StatusID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.StatusId);
            _params.Add(name: "@ResultID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.ResultId);

            _params.Add(name: "@CreateDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.CreateFromDate);
            _params.Add(name: "@CreateDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.CreateToDate);
            _params.Add(name: "@ModifiedDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.ModifyFromDate);
            _params.Add(name: "@ModifiedDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.ModifyToDate);

            _params.Add(name: "@CaseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.CaseId);
            _params.Add(name: "@ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.ProfilId);
            _params.Add(name: "@CustomerName", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.SearchedName);
            _params.Add(name: "@SourceID", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.AddrSourceId);
            _params.Add(name: "@MatchTranId", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.MatchTranId);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn.QueryAsync<CaseModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<CaseModel> _CaseModel = result.ToList<CaseModel>();

                return _CaseModel;
            }

        }

        public async Task<List<CaseModel>> CaseByStatus(CaseFilterBase caseFilter, int? ContactID)
        {
            var procedure = "REPORT_CaseByStatus";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.GateId);
            _params.Add(name: "@CaseTypeID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.CategoryId);
            _params.Add(name: "@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ContactID);

            _params.Add(name: "@CreateDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.CreateFromDate);
            _params.Add(name: "@CreateDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.CreateToDate);
            _params.Add(name: "@ModifiedDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.ModifyFromDate);
            _params.Add(name: "@ModifiedDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.ModifyToDate);

            _params.Add(name: "@CaseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.CaseId);
            _params.Add(name: "@ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.ProfilId);
            _params.Add(name: "@CustomerName", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.SearchedName);
            _params.Add(name: "@SourceID", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.AddrSourceId);
            _params.Add(name: "@MatchTranId", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.MatchTranId);


            using (var conn = new SqlConnection(_sqlConnStr))
                {
                    var result = await conn .QueryAsync<CaseModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CaseModel> _CaseModel = result.ToList<CaseModel>();

                    return _CaseModel;
                }
            
        }

        public async Task<List<CaseModel>> CaseByResult(CaseFilterBase caseFilter, int? ContactID)
        {
            var procedure = "REPORT_CaseByResult";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.GateId);
            _params.Add(name: "@CaseTypeID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.CategoryId);
            if (caseFilter.ShowOnlyOwnCases)
            {
                _params.Add(name: "@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ContactID);
            }
            

            _params.Add(name: "@CreateDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.CreateFromDate);
            _params.Add(name: "@CreateDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.CreateToDate);
            _params.Add(name: "@ModifiedDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.ModifyFromDate);
            _params.Add(name: "@ModifiedDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.ModifyToDate);

            _params.Add(name: "@CaseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.CaseId);
            _params.Add(name: "@ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.ProfilId);
            _params.Add(name: "@CustomerName", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.SearchedName);
            _params.Add(name: "@SourceID", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.AddrSourceId);
            _params.Add(name: "@MatchTranId", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.MatchTranId);

            using (var conn = new SqlConnection(_sqlConnStr))
                {
                    var result = await conn .QueryAsync<CaseModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CaseModel> _CaseModel = result.ToList<CaseModel>();

                    return _CaseModel;
                }
          
        }

        public async Task<List<CaseModel>> AliasCount(CaseFilterBase caseFilter, int? ContactID)
        {
            var procedure = "REPORT_CaseByAlias";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.GateId);
            _params.Add(name: "@CaseTypeID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.CategoryId);
            _params.Add(name: "@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ContactID);

            _params.Add(name: "@CreateDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.CreateFromDate);
            _params.Add(name: "@CreateDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.CreateToDate);
            _params.Add(name: "@ModifiedDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.ModifyFromDate);
            _params.Add(name: "@ModifiedDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.ModifyToDate);

            _params.Add(name: "@CaseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.CaseId);
            _params.Add(name: "@ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.ProfilId);
            _params.Add(name: "@CustomerName", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.SearchedName);
            _params.Add(name: "@SourceID", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.AddrSourceId);
            _params.Add(name: "@MatchTranId", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.MatchTranId);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn .QueryAsync<CaseModel>(procedure, _params, commandType: CommandType.StoredProcedure);                                      

                return result.ToList<CaseModel>();
            }
         
        }

        public async Task<List<CaseModel>> AliasDetail(CaseFilterBase caseFilter, int? ContactID)
        {
            var procedure = "REPORT_CaseAliasDetail";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.GateId);
            _params.Add(name: "@CaseTypeID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.CategoryId);
            _params.Add(name: "@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ContactID);
            _params.Add(name: "@StatusID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.StatusId);
            _params.Add(name: "@ResultID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.ResultId);

            _params.Add(name: "@CreateDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.CreateFromDate);
            _params.Add(name: "@CreateDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.CreateToDate);
            _params.Add(name: "@ModifiedDate1", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.ModifyFromDate);
            _params.Add(name: "@ModifiedDate2", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: caseFilter.ModifyToDate);

            _params.Add(name: "@CaseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.CaseId);
            _params.Add(name: "@ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseFilter.ProfilId);
            _params.Add(name: "@CustomerName", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.SearchedName);
            _params.Add(name: "@SourceID", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.AddrSourceId);
            _params.Add(name: "@MatchTranId", dbType: DbType.String, direction: ParameterDirection.Input, value: caseFilter.MatchTranId);

            using (var conn = new SqlConnection(_sqlConnStr))
                {
                    var result = await conn .QueryAsync<CaseModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CaseModel> _CaseModel = result.ToList<CaseModel>();

                    return _CaseModel;
                }
           
        }
    }
}
