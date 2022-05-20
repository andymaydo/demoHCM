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
    public class MailQueueService : IMailQueue
    {
        private string _sqlConnStr;


        public MailQueueService(IConfiguration conf)
        {
            _sqlConnStr = conf.GetSection("ConnectionStrings:Default").Value;
        }

        public async Task<List<MailQueueModel>> GetTop(int top)
        {
            string sql = "[dbo].[pMailQueue_Retrive]";

            var p = new DynamicParameters();

            p.Add("Top", top); 
           
            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                var result = await connection.QueryAsync<MailQueueModel>(sql, p, commandType: CommandType.StoredProcedure);
                return result.AsList();
            }
        }


        public async Task SetStatus(int recId, int statusId)
        {
            string sql = "[dbo].[pMailQueue_Update]";

            var p = new DynamicParameters();
            p.Add("@recId", recId);
            p.Add("@StatusId", statusId); //50: OK, 30: failed


            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
