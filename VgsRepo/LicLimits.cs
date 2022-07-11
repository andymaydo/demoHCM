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
using CryptDecryptLib;

namespace VgsRepo
{
    public class LicLimits : ILicLimits
    {
        private string _sqlConnStr;
        private CryptDecrypt _cryptDecrypt;
        public LicLimits(IConfiguration config)
        {
            _sqlConnStr = config.GetConnectionString("VGS");
            _cryptDecrypt = new CryptDecrypt("PO9$&!VCzae*%?<,[$");
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

                var usrCntCrypted = p.Get<String>("@UserCountLimit");

                var usrCntDecrypted = _cryptDecrypt.DecryptData(usrCntCrypted);

                return Int32.Parse(usrCntDecrypted);
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

                var prfCntCrypted = p.Get<String>("@ProfileCountLimit");

                var prfCntDecrypted = _cryptDecrypt.DecryptData(prfCntCrypted);

                return Int32.Parse(prfCntDecrypted);
            }
        }


        public async Task SetCurrentUserCount(string applianceId, int currUserCount)
        {
            var usrCntCrypted = _cryptDecrypt.EncryptData(currUserCount.ToString());

            string sql = "[dbo].[pHCMLicense_CurrentUsrCnt_Update]";

            var p = new DynamicParameters();
            p.Add("@UserCountCurrent", usrCntCrypted);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                await conn.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

            }
        }

        public async Task SetCurrentProfileCount(string applianceId, int currProfileCount)
        {
            var prfCntCrypted = _cryptDecrypt.EncryptData(currProfileCount.ToString());

            string sql = "[dbo].[pHCMLicense_CurrentProfileCnt_Update]";

            var p = new DynamicParameters();            
            p.Add("@ProfileCountCurrent", prfCntCrypted);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                await conn.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);
                
            }
        }
    }
}
