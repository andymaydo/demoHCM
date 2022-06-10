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
    public class UserRolesData : IUserRolesData
    {
        private string _sqlConnStr;
        public UserRolesData(IConfiguration config)
        {
            _sqlConnStr = config.GetConnectionString("Default");
        }

        public List<UserRolesModel> GetLocalizatedNames()
        {
            var procedure = "Roles_NamesLocalizatedActive";
            var _params = new DynamicParameters();


            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = conn.Query<UserRolesModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<UserRolesModel> _UserRolesModel = result.ToList<UserRolesModel>();

                return _UserRolesModel;
            }
            

        }

        public async Task<int> AddUserInRole(string roleName, string LoginName, int ModifyUserID)
        {
            var procedure = "userRole_AddUserInRole";
            var _params = new DynamicParameters();

            _params.Add(name: "@rolename", dbType: DbType.String, direction: ParameterDirection.Input, value: roleName);
            _params.Add(name: "@LoginName", dbType: DbType.String, direction: ParameterDirection.Input, value: LoginName);

            using (var conn = new SqlConnection(_sqlConnStr))
            {

                var result = await conn.ExecuteAsync(procedure, _params, commandType: CommandType.StoredProcedure);
                return 0;

            }

        }
        public async Task<int> DelUserInRole(string roleName, string LoginName, int ModifyUserID)
        {
            var procedure = "userRole_DelUserInRole";
            var _params = new DynamicParameters();


            _params.Add(name: "@rolename", dbType: DbType.String, direction: ParameterDirection.Input, value: roleName);
            _params.Add(name: "@LoginName", dbType: DbType.String, direction: ParameterDirection.Input, value: LoginName);

            using (var conn = new SqlConnection(_sqlConnStr))
            {

                var result = await conn.ExecuteAsync(procedure, _params, commandType: CommandType.StoredProcedure);
                return 0;

            }
           
        }

        public bool IsUserInRole(string username, string rolename)
        {

            if ( string.IsNullOrEmpty(username) || string.IsNullOrEmpty(rolename))
            {
                return false;
            }

            var procedure = "userRole_IsUserInRole";
            var _params = new DynamicParameters();


            _params.Add(name: "@rolename", dbType: DbType.String, direction: ParameterDirection.Input, value: rolename);
            _params.Add(name: "@LoginName", dbType: DbType.String, direction: ParameterDirection.Input, value: username);

            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var conn = new SqlConnection(_sqlConnStr))
            {

                var result =  conn.Query<UserRolesModel>(procedure, _params, commandType: CommandType.StoredProcedure);
                List<UserRolesModel> _UserRolesModel = result.ToList<UserRolesModel>();
                if ( _UserRolesModel.Count > 0 && _UserRolesModel[0].RoleCount > 0 )
                {
                    return true;
                }
                return false;

            }

        }
    }
}
