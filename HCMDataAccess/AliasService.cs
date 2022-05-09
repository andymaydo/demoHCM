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
using AliasManger.Interfaces;
using AliasManger.Models;

namespace HCMDataAccess
{
    public class AliasService : IAliasManager
    {
        private string _sqlConnStr;
  

        public AliasService(IConfiguration conf)
        {
            _sqlConnStr = conf.GetSection("ConnectionStrings:VGS").Value;
        }


        public async Task<List<AmAlias>> GetByFilterAsync(string aliasName, string alisStreet, string profilesCSV,
            int statusId, bool waitAuth, string accId)
        {
            string sql = "[dbo].[Alias_List_HCM]";

            var p = new DynamicParameters();

            p.Add("Status", statusId);          //-100 => all
            p.Add("AliasName", aliasName);
            p.Add("Address", alisStreet);
                              
            p.Add("WaitAuth", waitAuth);
            p.Add("licenseTable", profilesCSV);      

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

        public async Task<List<AmAliasReport>> ReportGetByFilterAsync(DateTime von, DateTime bis, bool allTime, 
                string aliasName, string alisStreet, string profilesCSV, string accId)
        {
            string sql = "[dbo].[Alias_Report_Grouped_HCM]";

            var p = new DynamicParameters();

            p.Add("von", von);
            p.Add("bis", bis);
            p.Add("allTime", allTime);
           
            p.Add("AliasName", aliasName);
            p.Add("AliasStreet", alisStreet);
           
            p.Add("licenseTable", profilesCSV);    

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                var result = await connection.QueryAsync<AmAliasReport>(sql, p, commandType: CommandType.StoredProcedure);
                return result.AsList<AmAliasReport>();
            }
        }





        public async Task ReActivateAsync(int aliasId, string reason, string callerLogin)
        {
            
            string sql = "[dbo].[AliasProtocol_ReActiveAction]";

            var p = new DynamicParameters();
            p.Add("userID", -10);
            p.Add("aliasID", aliasId);
            p.Add("Description", reason);
            p.Add("userLogin", callerLogin + "@hcm");
            p.Add("@return_value", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                var returnValue = p.Get<int>("return_value");
            }
        }
        public async Task DeActivateAsync(int aliasId, string reason, string callerLogin)
        {
            
            string sql = "[dbo].[AliasProtocol_DeActiveAction]";

            var p = new DynamicParameters();
            p.Add("userID", -10);
            p.Add("aliasID", aliasId);
            p.Add("Description", reason);
            p.Add("userLogin", callerLogin + "@hcm");
            p.Add("@return_value", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                var returnValue = p.Get<int>("return_value");
            }
        }
        public async Task DeleteAsync(int aliasId, string reason, string callerLogin)
        {
            //Result res = new Result();
            string sql = "[dbo].[AliasProtocol_DeteteAction]";

            var p = new DynamicParameters();
            p.Add("userID", -10);
            p.Add("aliasID", aliasId);
            p.Add("Description", reason);
            p.Add("userLogin", callerLogin + "@hcm");
            p.Add("@return_value", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                var returnValue = p.Get<int>("return_value");
            }
        }
        public async Task AuthorizeAsync(int aliasProtokolId, string reason, string callerLogin)
        {
            //Result res = new Result();
            string sql = "[dbo].[AliasProtocol_CommitAction]";

            var p = new DynamicParameters();
            p.Add("userID", -10);
            p.Add("aliasProtocolID", aliasProtokolId);
            p.Add("AuthNote", reason);
            p.Add("userLogin", callerLogin + "@hcm");
            p.Add("@return_value", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                var returnValue = p.Get<int>("return_value");
            }
        }
        public async Task DiscardAsync(int aliasProtokolId, string reason, string callerLogin)
        {
            //Result res = new Result();
            string sql = "[dbo].[AliasProtocol_DiscardAction]";

            var p = new DynamicParameters();
            p.Add("userID", -10);
            p.Add("aliasProtocolID", aliasProtokolId);
            p.Add("AuthNote", reason);
            p.Add("userLogin", callerLogin + "@hcm");
            p.Add("@return_value", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                var returnValue = p.Get<int>("return_value");
            }
        }


    }
}
