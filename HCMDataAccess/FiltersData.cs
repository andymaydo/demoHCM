using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HCMDataAccess.Models.FiltersModels;

namespace HCMDataAccess
{
    public class FiltersData : IFiltersData
    {
        private readonly IConfiguration _config;
        private readonly ISqlDataAccess _db;
        public FiltersData(IConfiguration config, ISqlDataAccess db)
        {
            _config = config;
            _db = db;
        }

        public List<FilterGateModel> GetGates()
        {
            var procedure = "CaseData_Application_GetList";
            var _params = new DynamicParameters();

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {
                    var result =  conn.Query<FilterGateModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<FilterGateModel> _FilterGateModel = result.ToList<FilterGateModel>();

                    return _FilterGateModel;
                }
            }
            catch ( Exception ex)
            {
                return null;
            }
        }
        public  List<FilterProfileModel> GetProfiles(int appID)
        {
            var procedure = "Profile_GetByApp";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = conn.Query<FilterProfileModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<FilterProfileModel> _FilterProfileModel = result.ToList<FilterProfileModel>();

                    return _FilterProfileModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public  List<FilterCategoryModel> GetCategories(int appID)
        {
            var procedure = "CaseType_GetByApp";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = conn.Query<FilterCategoryModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<FilterCategoryModel> _FilterCategoryModel = result.ToList<FilterCategoryModel>();

                    return _FilterCategoryModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<FilterStatusModel> GetStatuses()
        {
            var procedure = "CaseStatus_GetActiveOnly";
            var _params = new DynamicParameters();

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = conn.Query<FilterStatusModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<FilterStatusModel> _FilterStatusModel = result.ToList<FilterStatusModel>();

                    return _FilterStatusModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
