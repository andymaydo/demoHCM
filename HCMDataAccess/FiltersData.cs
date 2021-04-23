﻿using Dapper;
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

        public List<GateModel> GetGates()
        {
            var procedure = "CaseData_Application_GetList";
            var _params = new DynamicParameters();

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {
                    var result =  conn.Query<GateModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<GateModel> _FilterGateModel = result.ToList<GateModel>();

                    return _FilterGateModel;
                }
            }
            catch ( Exception ex)
            {
                return null;
            }
        }
        public  List<ProfileModel> GetProfiles(int appID)
        {
            var procedure = "Profile_GetByApp";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = conn.Query<ProfileModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<ProfileModel> _FilterProfileModel = result.ToList<ProfileModel>();

                    return _FilterProfileModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public  List<CategoryModel> GetCategories(int appID)
        {
            var procedure = "CaseType_GetByApp";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = conn.Query<CategoryModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CategoryModel> _FilterCategoryModel = result.ToList<CategoryModel>();

                    return _FilterCategoryModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

         public List<CaseResultModel> GetResults(int appID)
        {
            var procedure = "CaseData_Result_4Application";
            var _params = new DynamicParameters();

            _params.Add(name: "@appID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: appID);

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = conn.Query<CaseResultModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CaseResultModel> _FilterResultsModel = result.ToList<CaseResultModel>();

                    return _FilterResultsModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<CaseStatusModel> GetStatuses()
        {
            var procedure = "CaseStatus_GetActiveOnly";
            var _params = new DynamicParameters();

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = conn.Query<CaseStatusModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CaseStatusModel> _FilterStatusModel = result.ToList<CaseStatusModel>();

                    return _FilterStatusModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<CaseStatusModel> GetStatusesForEscalation()
        {
            var procedure = "CaseStatus_GetForEscalation";
            var _params = new DynamicParameters();

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = conn.Query<CaseStatusModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CaseStatusModel> _FilterStatusModel = result.ToList<CaseStatusModel>();

                    return _FilterStatusModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<ProfileStatusModel> GetProfileStatuses()
        {
            var procedure = "ProfileStatus_GetList";
            var _params = new DynamicParameters();

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = conn.Query<ProfileStatusModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<ProfileStatusModel> _ProfileStatusModel = result.ToList<ProfileStatusModel>();

                    return _ProfileStatusModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
