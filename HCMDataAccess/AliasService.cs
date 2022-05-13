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

        public async Task CreateAsync(string aliasName, string aliasStreet, string description, 
                int? profilId, string sapIp, string sapGw, string sapMandant, string sapBelegNr,
                string caseUrl, string hcmUserFullName, string accId)
        {
            int licId;

            if(profilId != null)
            {
                licId = await GetLicIdByProfileId((int)profilId);
            }
            else
            {
                licId = await GetLicIdBySapInfo(sapIp, sapGw, sapMandant);
            }

            if(licId == -1)
            {
                throw new Exception("LicIdNotDetermined");
            }

            var existingAliasId = await AliasExists(aliasName, aliasStreet, licId, sapBelegNr);
            if (existingAliasId == 0)
            {
                await DbCreateAsync(aliasName, aliasStreet, description, hcmUserFullName,
                    sapBelegNr, caseUrl, licId, accId);
            }
            else
            {
                throw new Exception("AliasExists");
            }
        }





        private async Task<int>GetLicIdByProfileId(int profileId) 
        {
            string sql = "[dbo].[pLiz_RetriveBy_ProfileID]";

            var p = new DynamicParameters();
            p.Add("ProfileID", profileId);
            p.Add("LicID", DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                return p.Get<Int32>("LicID");
            }
        }

        private async Task<int> GetLicIdBySapInfo(string sapIp,string sapGw, string sapMandant)
        {
            string sql = "[dbo].[pLiz_RetriveBy_SapIP_SapGTW_Mandant]";

            var p = new DynamicParameters();
            p.Add("SapIP", sapIp);
            p.Add("SapGTW", sapGw);
            p.Add("Mandant", sapMandant);
            p.Add("LicID", DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                return p.Get<Int32>("LicID");
            }
        }

        private async Task DbCreateAsync(string aliasName, string aliasStreet, string description, string hcmUserFullName,
               string sapBelegNr, string caseUrl, int licId, string accId)
        {
            string sql = "[dbo].[AliasProtocol_InsertAndActivate]";

            var p = new DynamicParameters();
            p.Add("userID", -10);
            p.Add("AliasName", aliasName);
            p.Add("aliasAddress", aliasStreet);
            p.Add("Description", description);
            p.Add("hcmUserFullName", hcmUserFullName);
            p.Add("belegNummer", sapBelegNr);
            p.Add("CaseURL", caseUrl);
            p.Add("LicID", licId);
            p.Add("accId", accId);


            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);
            }
        }

        private async Task<int> AliasExists(string aliasName, string aliasStreet, int licId, string sapBelegNr)
        {
            string sql = "[dbo].[Alias_IsExists]";

            var p = new DynamicParameters();
            p.Add("AliasName", aliasName);
            p.Add("aliasAddress", aliasStreet);
            p.Add("checkDeleted", 0);
            p.Add("belegNummer", sapBelegNr);
            p.Add("LicID", licId);
            p.Add("@return_value", DbType.Int32, direction: ParameterDirection.ReturnValue);

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                await connection.ExecuteAsync(sql, p, commandType: CommandType.StoredProcedure);

                return p.Get<int>("return_value");
            }
        }
    }
}
