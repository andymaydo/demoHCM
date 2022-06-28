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

namespace VgsRepo
{
    public class LicLimits : ILicLimits
    {
        private string _sqlConnStr;
        public LicLimits(IConfiguration config)
        {
            _sqlConnStr = config.GetConnectionString("VGS");
        }

        public async Task<int> GetUserCountLimit(string applianceId)
        {
            string sql = "[dbo].[pHCMLicense_Retrieve]";

            var p = new DynamicParameters();
            p.Add("@UserCountLimit", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            p.Add("@ProfileCountLimit", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                await conn.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                var usrCnt = p.Get<String>("@UserCountLimit");

                return Int32.Parse(usrCnt);
            }

        }

        public async Task<int> GetProfileCountLimit(string applianceId)
        {
            string sql = "[dbo].[pHCMLicense_Retrieve]";

            var p = new DynamicParameters();
            p.Add("@UserCountLimit", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            p.Add("@ProfileCountLimit", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                await conn.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                var prfCnt = p.Get<String>("@ProfileCountLimit");

                return Int32.Parse(prfCnt);
            }
        }


        public async Task SetCurrentUserCount(string applianceId, int currUserCount)
        {
            string sql = "[dbo].[pHCMLicense_CurrentUsrCnt_Update]";

            var p = new DynamicParameters();
            p.Add("@UserCountCurrent", currUserCount);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                await conn.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

            }
        }

        public async Task SetCurrentProfileCount(string applianceId, int currProfileCount)
        {
            string sql = "[dbo].[pHCMLicense_CurrentProfileCnt_Update]";

            var p = new DynamicParameters();            
            p.Add("@ProfileCountCurrent", currProfileCount);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                await conn.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);
                
            }
        }
    }
}
