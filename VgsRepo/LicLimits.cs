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

        public async Task<int> UserCount(string applianceId)
        {
            string sql = "[dbo].[pHCMLicense_Retrieve]";

            var p = new DynamicParameters();
            p.Add("@UserCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@ProfileCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn.QueryAsync(sql, p, commandType: CommandType.StoredProcedure);

                var usrCnt = p.Get<Int32>("@UserCount");

                return usrCnt;
            }

        }

        public async Task<int> ProfileCount(string applianceId)
        {
            string sql = "[dbo].[pHCMLicense_Retrieve]";

            var p = new DynamicParameters();
            p.Add("@UserCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@ProfileCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn.QueryAsync(sql, p, commandType: CommandType.StoredProcedure);

                var prfCnt = p.Get<Int32>("@ProfileCount");

                return prfCnt;
            }
        }
    }
}
