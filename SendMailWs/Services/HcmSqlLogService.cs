using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SendMailWs.Interfaces;

namespace SendMailWs.Services
{
    public class HcmSqlLogService : ISqlLog
    {
        private string _sqlConnStr;
        private readonly ILogger<HcmSqlLogService> _logger;


        public HcmSqlLogService(IConfiguration conf, ILogger<HcmSqlLogService> logger)
        {
            _sqlConnStr = conf.GetSection("ConnectionStrings:Default").Value;
            _logger = logger;
        }

        public async Task AddSqlLog(string errCode, string errMsg, Exception ex)
        {
            var evMessage = ex.StackTrace ?? errMsg ?? "";

            int evCode;
            Int32.TryParse(errCode, out evCode);

            await DbCreateEventLog(evCode, evMessage);

        }

        private async Task DbCreateEventLog(int evCode, string evMessage)
        {
            string sql = "[dbo].[pErrLog_Create]";

            var p = new DynamicParameters();
            p.Add("@ErrCode", evCode);
            p.Add("@ErrMessage", evMessage);


            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
