using Application.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.FiltersModels;

namespace DbRepo
{
    public class FiltersData : IFiltersData
    {
        private string _sqlConnStr;
        public FiltersData(IConfiguration config)
        {
            _sqlConnStr = config.GetConnectionString("Default");
        }

        public List<GateModel> GetGates()
        {
            var procedure = "CaseData_Application_GetList";
            var _params = new DynamicParameters();
         
            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result =  conn.Query<GateModel>(procedure, _params, commandType: CommandType.StoredProcedure);                
                return result.ToList();
            }
         
            
        }
        
        public  List<ProfileModel> GetProfiles(int? contactID)
        {
            var procedure = "Profile_GetByContact";
            var _params = new DynamicParameters();

            _params.Add(name: "@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: contactID);


            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = conn.Query<ProfileModel>(procedure, _params, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }

        }
        
        public  List<CategoryModel> GetCategories(int appID)
        {
            var procedure = "CaseType_GetByApp";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);


            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = conn.Query<CategoryModel>(procedure, _params, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }

        }

        public List<CaseResultModel> GetResults(int appID)
        {
            var procedure = "CaseData_Result_4Application";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);

            
            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = conn.Query<CaseResultModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<CaseResultModel> _FilterResultsModel = result.ToList<CaseResultModel>();

                return _FilterResultsModel;
            }
            
        }

        public List<CaseStatusModel> GetStatuses()
        {
            var procedure = "CaseStatus_GetActiveOnly";
            var _params = new DynamicParameters();

            
            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = conn.Query<CaseStatusModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<CaseStatusModel> _FilterStatusModel = result.ToList<CaseStatusModel>();

                return _FilterStatusModel;
            }
            
        }

        public List<CaseStatusModel> GetStatuses4Application(int appID)
        {
            var procedure = "CaseData_Status_4Application";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);

        
            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = conn.Query<CaseStatusModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<CaseStatusModel> _FilterStatusModel = result.ToList<CaseStatusModel>();

                return _FilterStatusModel;
            }
            
        }

        public List<CaseStatusModel> GetStatusesForEscalation()
        {
            var procedure = "CaseStatus_GetForEscalation";
            var _params = new DynamicParameters();

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = conn.Query<CaseStatusModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<CaseStatusModel> _FilterStatusModel = result.ToList<CaseStatusModel>();

                return _FilterStatusModel;
            }
            
        }

        public List<ProfileStatusModel> GetProfileStatuses()
        {
            var procedure = "ProfileStatus_GetList";
            var _params = new DynamicParameters();

   
            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = conn.Query<ProfileStatusModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<ProfileStatusModel> _ProfileStatusModel = result.ToList<ProfileStatusModel>();

                return _ProfileStatusModel;
            }
           
        }
    }
}
