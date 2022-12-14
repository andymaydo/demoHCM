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
    public class UsersData : IUsersData
    {
        private string _sqlConnStr;
        public UsersData(IConfiguration config)
        {
            _sqlConnStr = config.GetConnectionString("Default");
        }

        public async Task<List<UsersModel>> UsersList(int userID)
        {
            var procedure = "Users_GetList";
            var _params = new DynamicParameters();

            _params.Add(name: "@userID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: userID);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn.QueryAsync<UsersModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<UsersModel> _UsersModel = result.ToList<UsersModel>();

                return _UsersModel;
            }

        }

        public async Task<int> SaveData(int userID, string Email, string FullName, string Department, bool Enable, int ModifyUserID)
        {
            var procedure = "USERS_EDIT";
            var _params = new DynamicParameters();

            _params.Add(name: "@userID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: userID);
            _params.Add(name: "@FullName", dbType: DbType.String, direction: ParameterDirection.Input, value: FullName);
            _params.Add(name: "@email", dbType: DbType.String, direction: ParameterDirection.Input, value: Email);
            _params.Add(name: "@Department", dbType: DbType.String, direction: ParameterDirection.Input, value: Department);
            _params.Add(name: "@Enable", dbType: DbType.Boolean, direction: ParameterDirection.Input, value: Enable);
            _params.Add(name: "@modifyUserID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ModifyUserID);

            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_sqlConnStr))
                {

                    var result = await conn.ExecuteAsync(procedure, _params, commandType: CommandType.StoredProcedure);
                    int ReturnValue = _params.Get<int>("@ReturnValue");
                    return ReturnValue;

                }
            }
            catch
            {
                return -1;
            }
        }

        public async Task<int> CreateUser(string LoginName, string Password, string Email, string FullName, string Department, bool Enable, int ModifyUserID)
        {
            var procedure = "USERS_CREATE";
            var _params = new DynamicParameters();

            _params.Add(name: "@LoginName", dbType: DbType.String, direction: ParameterDirection.Input, value: LoginName);
            _params.Add(name: "@Password", dbType: DbType.String, direction: ParameterDirection.Input, value: Password);
            _params.Add(name: "@email", dbType: DbType.String, direction: ParameterDirection.Input, value: Email);
            _params.Add(name: "@FullName", dbType: DbType.String, direction: ParameterDirection.Input, value: FullName);
            _params.Add(name: "@Department", dbType: DbType.String, direction: ParameterDirection.Input, value: Department);
            _params.Add(name: "@Enable", dbType: DbType.Boolean, direction: ParameterDirection.Input, value: Enable);
            _params.Add(name: "@modifyUserID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ModifyUserID);

            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

           
            using (var conn = new SqlConnection(_sqlConnStr))
            {

                var result = await conn.ExecuteAsync(procedure, _params, commandType: CommandType.StoredProcedure);
                int ReturnValue = _params.Get<int>("@ReturnValue");
                return ReturnValue;

            }
            
        }

        public async Task<int> ChangePassword(int UserID, string newPass, int ModifyUserID)
        {
            var procedure = "USERS_CHANGEPASSWORD";
            var _params = new DynamicParameters();

            _params.Add(name: "@userID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: UserID);
            _params.Add(name: "@newPass", dbType: DbType.String, direction: ParameterDirection.Input, value: newPass);
            _params.Add(name: "@modifyUserID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ModifyUserID);

            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

           
            using (var conn = new SqlConnection(_sqlConnStr))
            {

                var result = await conn.ExecuteAsync(procedure, _params, commandType: CommandType.StoredProcedure);
                int ReturnValue = _params.Get<int>("@ReturnValue");
                return ReturnValue;

            }
           
        }

        public async Task<int> DeleteUser(int UserID, int ModifyUserID)
        {
            var procedure = "USERS_DELETE";
            var _params = new DynamicParameters();

            _params.Add(name: "@userID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: UserID);
            _params.Add(name: "@modifyUserID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: ModifyUserID);

            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            
            using (var conn = new SqlConnection(_sqlConnStr))
            {

                var result = await conn.ExecuteAsync(procedure, _params, commandType: CommandType.StoredProcedure);
                int ReturnValue = _params.Get<int>("@ReturnValue");
                return ReturnValue;

            }
             
        }

    }
}
