using Dapper;
using HCMModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMDataAccess
{
    public class LoginData : ILoginData
    {
        private string _sqlConnStr;
        public LoginData(IConfiguration config)
        {
            _sqlConnStr = config.GetConnectionString("Default");
        }

        public async Task<LoginResponse> Login(string username, string password)
        {
            LoginResponse _LoginResponse = new LoginResponse();

                var procedure = "USERS_LOGIN";
                var _params = new DynamicParameters();

                _params.Add(name: "@UserName", dbType: DbType.String, direction: ParameterDirection.Input, value: username);
                _params.Add(name: "@Password", dbType: DbType.String, direction: ParameterDirection.Input, value: password);
                _params.Add(name: "@FullName", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                _params.Add(name: "@NikName", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                _params.Add(name: "@contactID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                _params.Add(name: "@UserID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);


            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn.QueryAsync<LoginResponse>(procedure, _params, commandType: CommandType.StoredProcedure);
                int ReturnValue = _params.Get<int>("@ReturnValue");


                if (ReturnValue == 0)
                {

                    //_LoginResponse = result.ToList<LoginResponse>()[0];
                    _LoginResponse.FullName = _params.Get<String>("@FullName");
                    _LoginResponse.NikName = _params.Get<String>("@NikName");
                    _LoginResponse.ContactID = _params.Get<Int32>("@contactID");
                    _LoginResponse.UserID = _params.Get<Int32>("@UserID");
                    _LoginResponse.Success = true;
                    _LoginResponse.ErrCode = 0;
                }
                else
                {
                    _LoginResponse.Success = false;
                    _LoginResponse.ErrCode = 0;
                }

                return _LoginResponse;
            }

        }

        public async Task<List<string>> UserRolesGetAsync(string userLogin)
        {
            string sql = "[dbo].[userRole_GetRolesForUser]";

            var p = new DynamicParameters();
            p.Add("LoginName", userLogin);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                var result = await connection.QueryAsync<string>(sql, p, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
    }
}
