using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HCMApi
{
    public class CMSEvent : ICMSEvent
    {
        public string EventID { get; set; }
        public string EventName { get; set; }
        public string AppID { get; set; }
        public int CaseStatusID { get; set; }
        public int CaseResultID { get; set; }

        private readonly IConfiguration _config;

        public CMSEvent(IConfiguration config)
        {
            _config = config;
        }

        public CMSEvent()
        {
            EventID = "";
            EventName = "";
            AppID = "";
            CaseStatusID = 0;
            CaseResultID = 0;
        }

        public async Task<CMSEvent> Load(string _eventID)
        {

            var procedure = "Event_GetOne";
            var _params = new DynamicParameters();

            _params.Add(name: "@eventID", dbType: DbType.String, direction: ParameterDirection.Input, value: _eventID);

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = await conn.QueryAsync<CMSEvent>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CMSEvent> _CMSEventList = result.ToList<CMSEvent>();
                    if (_CMSEventList.Count > 0)
                    {
                        CMSEvent _CMSEvent = new CMSEvent();
                        _CMSEvent = _CMSEventList[0];


                        return _CMSEvent;
                    }
                    return null;
                }
            }
            catch //(Exception ex)
            {
                return null;
            }


        }

        

        
    }
}
