using Dapper;
using HCMDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMDataAccess
{
    public class UserRolesData : IUserRolesData
    {
        private readonly ISqlDataAccess _db;
        public UserRolesData(ISqlDataAccess db)
        {
            _db = db;
        }

        public List<UserRolesModel> GetLocalizatedNames()
        {
            var procedure = "Roles_NamesLocalizatedActive";
            var _params = new DynamicParameters();

            //_params.Add(name: "@userID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: userID);
            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {
                    var result = conn.Query<UserRolesModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<UserRolesModel> _UserRolesModel = result.ToList<UserRolesModel>();

                    return _UserRolesModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> AddUserInRole(string roleName, string LoginName, int ModifyUserID)
        {
            var procedure = "userRole_AddUserInRole";
            var _params = new DynamicParameters();


            _params.Add(name: "@rolename", dbType: DbType.String, direction: ParameterDirection.Input, value: roleName);
            _params.Add(name: "@LoginName", dbType: DbType.String, direction: ParameterDirection.Input, value: LoginName);
            //_params.Add(name: "@modifyUserID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ModifyUserID);

            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {

                    var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);
                    int ReturnValue = _params.Get<int>("@ReturnValue");
                    return ReturnValue;

                }
            }
            catch
            {
                return -1;
            }
        }
        public async Task<int> DelUserInRole(string roleName, string LoginName, int ModifyUserID)
        {
            var procedure = "userRole_DelUserInRole";
            var _params = new DynamicParameters();


            _params.Add(name: "@rolename", dbType: DbType.String, direction: ParameterDirection.Input, value: roleName);
            _params.Add(name: "@LoginName", dbType: DbType.String, direction: ParameterDirection.Input, value: LoginName);
            //_params.Add(name: "@modifyUserID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ModifyUserID);

            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {

                    var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);
                    int ReturnValue = _params.Get<int>("@ReturnValue");
                    return ReturnValue;

                }
            }
            catch
            {
                return -1;
            }
        }
    }
}
