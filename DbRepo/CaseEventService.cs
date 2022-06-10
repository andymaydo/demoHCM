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

namespace DbRepo
{
    public class CaseEventService : ICaseEvent
    {
        private string _sqlConnStr;

        public CaseEventService(IConfiguration config)
        {
            _sqlConnStr = config.GetConnectionString("Default");

        }
        public async Task<List<CaseEvent>> GetHistoryByCase(int CaseID)
        {
            var procedure = "CaseEvent_GetAll";
            var _params = new DynamicParameters();

            _params.Add(name: "@caseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseID);


            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn.QueryAsync<CaseEvent>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<CaseEvent> _CaseEvent = result.ToList<CaseEvent>();

                return _CaseEvent;
            }

        }

        public async Task<int> Create(CaseEvent caseEv)
        {

            var procedure = "CaseEvent_Create";
            var _params = new DynamicParameters();

            _params.Add(name: "@caseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseEv.caseID);
            _params.Add(name: "@eventID", dbType: DbType.String, direction: ParameterDirection.Input, value: caseEv.eventID);
            _params.Add(name: "@IssuedByID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseEv.IssuedByID);
            _params.Add(name: "@CaseEventData", dbType: DbType.Xml, direction: ParameterDirection.Input, value: caseEv.CaseEventData);
            _params.Add(name: "@CaseEventNotifyContacts", dbType: DbType.Xml, direction: ParameterDirection.Input, value: caseEv.CaseEventNotifyContacts);
            _params.Add(name: "@CaseEventText", dbType: DbType.String, direction: ParameterDirection.Input, value: caseEv.CaseEventText);

            using (var conn = new SqlConnection(_sqlConnStr))
            {

                var result = await conn.QueryAsync<CaseEvent>(procedure, _params, commandType: CommandType.StoredProcedure);

                CaseEvent _CaseEvent = result.First();

                //if (_CaseEvent.CaseEventID > 0)
                //    CaseEventHandlers.OnEvent(_CaseEvent);

                return _CaseEvent.CaseEventID;

            }

        }

        public async Task SendMailToNotifyList(int caseEventID, string contacts)
        {

            var procedure = "CaseEvent_SendMailToNotifyList";
            var _params = new DynamicParameters();

            _params.Add(name: "@CaseEventID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseEventID);
            _params.Add(name: "@CaseEventNotifyContacts", dbType: DbType.Xml, direction: ParameterDirection.Input, value: contacts);

            using (var conn = new SqlConnection(_sqlConnStr))
            {

                var affectedRows = await conn.ExecuteAsync(procedure, _params, commandType: CommandType.StoredProcedure);

            }

        }
    }
}
