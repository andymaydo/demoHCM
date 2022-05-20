using Dapper;
using HCMDataAccess;
using HCMModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SendMailWs.Services
{
    public class SettingsService : ISettingsData
    {
        private string _sqlConnStr;


        public SettingsService(IConfiguration conf)
        {
            _sqlConnStr = conf.GetSection("ConnectionStrings:Default").Value;
        }

        public async Task<SettingsModel> GetSettings()
        {
            string sql = "[dbo].[AppSettings_Get]";

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                var result = await connection.QueryAsync<SettingsModel>(sql, commandType: CommandType.StoredProcedure);
                return result.First();
            }
        }

        public async Task<int> ChangeSettings(int IssuedByID, string SmtpServer, string SmtpPort, bool isAuth, string SmtpUser, string SmtpPwd)
        {
           throw new NotImplementedException();
        }
    }
}
