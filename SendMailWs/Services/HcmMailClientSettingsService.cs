using Dapper;
using MailLib.Models;
using SendMailWs.Interfaces;
using SendMailWs.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SendMailWs.Services
{
    public class HcmMailClientSettingsService : IMailClientSettings
    {
        private readonly string _sqlConnStr;
        private readonly bool _enableTls;


        public HcmMailClientSettingsService(IConfiguration conf)
        {
            _sqlConnStr = conf.GetSection("ConnectionStrings:Default").Value;
            _enableTls = conf.GetSection("sppMailSettings:EnableTls").Get<bool>();
        }

        public async Task<MailClientSettings> GetSettings()
        {
            string sql = "[dbo].[AppSettings_Get]";

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                var orgResult = await connection.QueryAsync<HcmSettingsModel>(sql, commandType: CommandType.StoredProcedure);

                var targetRes = orgResult
                    .Select(x => new MailClientSettings()
                    { 
                        EnableTls = _enableTls, 
                        Host = x.SMTPServer, Port = Int32.Parse(x.SMTPServerPort), 
                        UseAuthentication = x.SMTPServerAuth, 
                        Login = x.SMTPServerUser, Password = x.SMTPServerPass
                    });

                return targetRes.First();
            }
        }

        private class HcmSettingsModel
        {

            public string SMTPServer { get; set; }
            public string SMTPServerPort { get; set; }
            public bool SMTPServerAuth { get; set; }
            public string SMTPServerUser { get; set; }
            public string SMTPServerPass { get; set; }

            public string AppUrl { get; set; }

            public HcmSettingsModel()
            {
                SMTPServer = String.Empty;
                SMTPServerPort = String.Empty;
                
                SMTPServerUser = String.Empty;
                SMTPServerPass = String.Empty;

                AppUrl = String.Empty;
            }
        }
    }
}
