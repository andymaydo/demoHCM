using Dapper;
using HCMModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMDataAccess
{
    public class SettingsData : ISettingsData
    {     


        private readonly ISqlDataAccess _db;
        public SettingsData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<SettingsModel>> GetSettings()
        {
            var procedure = "AppSettings_Get";
            var _params = new DynamicParameters();

            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {
                    var result = conn.Query<SettingsModel>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<SettingsModel> _SettingsModel = result.ToList<SettingsModel>();

                    return _SettingsModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> ChangeSettings(int IssuedByID, string SmtpServer, int SmtpPort, bool isAuth, string SmtpUser, string SmtpPwd)
        {
            var procedure = "pAppSettings_UpdateSMTP";
            var _params = new DynamicParameters();

            _params.Add(name: "@SMTPServer", dbType: DbType.String, direction: ParameterDirection.Input, value: SmtpServer);
            _params.Add(name: "@SMTPPort", dbType: DbType.String, direction: ParameterDirection.Input, value: SmtpPort.ToString());
            _params.Add(name: "@SMTPAuth", dbType: DbType.Boolean, direction: ParameterDirection.Input, value: isAuth);
            _params.Add(name: "@SMTPUser", dbType: DbType.String, direction: ParameterDirection.Input, value: SmtpUser);
            _params.Add(name: "@SMTPPass", dbType: DbType.String, direction: ParameterDirection.Input, value: SmtpPwd);
            _params.Add(name: "@modifyUserID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: IssuedByID);

            _params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                using (var conn = new SqlConnection(_db.GetConnStrName()))
                {

                    var result = await conn.QueryAsync<object>(procedure, _params, commandType: CommandType.StoredProcedure);
                    int ReturnValue = _params.Get<int>("@ReturnValue");
                    return ReturnValue;

                }
            }
            catch
            {
                return -1;
            }
        }
    }
}
