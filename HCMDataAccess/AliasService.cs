using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCMModels;

namespace HCMDataAccess
{
    public class AliasService : IAliasManager
    {
        private string _sqlConnStr;
  

        public AliasService(IConfiguration conf)
        {
            _sqlConnStr = conf.GetSection("ConnectionStrings:VGS").Value;
        }


        public async Task<List<AmAlias>> GetByFilterAsync(AmAliasFilter filter, int userId)
        {
            string sql = "[dbo].[Alias_List]";

            var p = new DynamicParameters();
            //p.Add("UserID", userId);
            //p.Add("Status", filter.StatusId);          //-100 => all
            //p.Add("AliasName", filter.Name);
            //p.Add("Address", filter.Street);
            //p.Add("LicID", -1);                     // -1 => all
            //p.Add("WaitAuth", filter.WaitAuth);
            //p.Add("APPID", filter.LicenseId);       // "-1" => all; -10: uebergreifend

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                var result = await connection.QueryAsync<AmAlias>(sql, p, commandType: CommandType.StoredProcedure);
                return result.AsList<AmAlias>();
            }
        }
        public async Task<AmAlias> GetByIdAsync(int aliasId)
        {
            string sql = "[dbo].[pAlias_GetById]";

            var p = new DynamicParameters();
            p.Add("AliasId", aliasId);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                var result = await connection.QueryFirstAsync<AmAlias>(sql, p, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<List<AmAliasProtocol>> ProtocolGetWaitingByAliasIdAsync(int aliasId)
        {
            string sql = "[dbo].[pAliasHistory_GetWaitingAuthByAliasId]";

            var p = new DynamicParameters();
            p.Add("AliasId", aliasId);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                var result = await connection.QueryAsync<AmAliasProtocol>(sql, p, commandType: CommandType.StoredProcedure);
                return result.AsList<AmAliasProtocol>();
            }
        }
        public async Task<List<AmAliasProtocol>> ProtocolGetByAliasIdAsync(int aliasId)
        {
            string sql = "[dbo].[pAliasHistory_Retrive]";

            var p = new DynamicParameters();
            p.Add("AliasId", aliasId);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                var result = await connection.QueryAsync<AmAliasProtocol>(sql, p, commandType: CommandType.StoredProcedure);
                return result.AsList<AmAliasProtocol>();
            }
        }
        public async Task<List<AmAliasReport>> ReportGetByFilterAsync(AmAliasFilter filter, int userId)
        {
            string sql = "[CLOUD].[Alias_Report_Grouped]";

            var p = new DynamicParameters();

            p.Add("von", filter.Von);
            p.Add("bis", filter.Bis);
            p.Add("allTime", filter.AllTime);
            p.Add("LizID", -1);                     // -1 => all
            p.Add("AliasName", filter.Name);
            p.Add("AliasStreet", filter.Street);
            p.Add("callerID", userId);
            p.Add("APPID", filter.LicenseId);       // "-1" => all; -10: uebergreifend

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                var result = await connection.QueryAsync<AmAliasReport>(sql, p, commandType: CommandType.StoredProcedure);
                return result.AsList<AmAliasReport>();
            }
        }





        public async Task ReActivateAsync(int aliasId, string reason, int callerId)
        {
            
            string sql = "[dbo].[AliasProtocol_ReActiveAction]";

            var p = new DynamicParameters();
            p.Add("userID", callerId);
            p.Add("aliasID", aliasId);
            p.Add("Description", reason);
            p.Add("@return_value", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                var returnValue = p.Get<int>("return_value");

                //if (returnValue == 0)
                //{
                //    res.Success = true;
                //}
                //else
                //{
                //    res.Success = false;
                //    res.ErrMsg = "SQLErr";
                //}

                //return res;
            }
        }
        public async Task DeActivateAsync(int aliasId, string reason, int callerId)
        {
            
            string sql = "[dbo].[AliasProtocol_DeActiveAction]";

            var p = new DynamicParameters();
            p.Add("userID", callerId);
            p.Add("aliasID", aliasId);
            p.Add("Description", reason);
            p.Add("@return_value", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                var returnValue = p.Get<int>("return_value");

                //if (returnValue == 0)
                //{
                //    res.Success = true;
                //}
                //else
                //{
                //    res.Success = false;
                //    res.ErrMsg = "SQLErr";
                //}

                //return res;
            }
        }
        public async Task DeleteAsync(int aliasId, string reason, int callerId)
        {
            //Result res = new Result();
            string sql = "[dbo].[AliasProtocol_DeteteAction]";

            var p = new DynamicParameters();
            p.Add("userID", callerId);
            p.Add("aliasID", aliasId);
            p.Add("Description", reason);
            p.Add("@return_value", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                var returnValue = p.Get<int>("return_value");

                //if (returnValue == 0)
                //{
                //    res.Success = true;
                //}
                //else
                //{
                //    res.Success = false;
                //    res.ErrMsg = "SQLErr";
                //}

                //return res;
            }
        }
        public async Task AuthorizeAsync(int aliasProtokolId, string reason, int callerId)
        {
            //Result res = new Result();
            string sql = "[dbo].[AliasProtocol_CommitAction]";

            var p = new DynamicParameters();
            p.Add("userID", callerId);
            p.Add("aliasProtocolID", aliasProtokolId);
            p.Add("AuthNote", reason);
            p.Add("@return_value", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                var returnValue = p.Get<int>("return_value");

                //if (returnValue == 0)
                //{
                //    res.Success = true;
                //}
                //else
                //{
                //    res.Success = false;
                //    res.ErrMsg = "SQLErr";
                //}

                //return res;
            }
        }
        public async Task DiscardAsync(int aliasProtokolId, string reason, int callerId)
        {
            //Result res = new Result();
            string sql = "[dbo].[AliasProtocol_DiscardAction]";

            var p = new DynamicParameters();
            p.Add("userID", callerId);
            p.Add("aliasProtocolID", aliasProtokolId);
            p.Add("AuthNote", reason);
            p.Add("@return_value", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                var returnValue = p.Get<int>("return_value");

                //if (returnValue == 0)
                //{
                //    res.Success = true;
                //}
                //else
                //{
                //    res.Success = false;
                //    res.ErrMsg = "SQLErr";
                //}

                //return res;
            }
        }


    }
}
