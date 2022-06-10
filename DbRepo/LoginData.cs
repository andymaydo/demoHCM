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
    public class LoginData : ILoginData
    {
        private string _sqlConnStr;
        public LoginData(IConfiguration config)
        {
            _sqlConnStr = config.GetConnectionString("Default");
        }

        public async Task<LoginResponse> Login(string username, string password)
        {
            string sql = "[dbo].[USERS_LOGIN]";

            var p = new DynamicParameters();
            p.Add("UserName", username);
            p.Add("Password", password);
            p.Add("FullName", dbType: DbType.String, direction: ParameterDirection.Output, size:100);
            p.Add("NikName", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
            p.Add("contactID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("UserID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            LoginResponse _LoginResponse = new LoginResponse();

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn.QueryAsync(sql, p, commandType: CommandType.StoredProcedure);
                int ReturnValue = p.Get<int>("@ReturnValue");


                if (ReturnValue == 0)
                {
                    _LoginResponse.FullName = p.Get<String>("@FullName");
                    _LoginResponse.NikName = p.Get<String>("@NikName");
                    _LoginResponse.ContactID = p.Get<Int32>("@contactID");
                    _LoginResponse.UserID = p.Get<Int32>("@UserID");
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
