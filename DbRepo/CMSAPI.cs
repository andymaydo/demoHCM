using Application.Interfaces;
using Dapper;
using Domain;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DbRepo
{
    public class CMSAPI : ICMSAPI
    {
        private string _sqlConnStr;
        private ICaseEvent _caseEventService;

        public CMSAPI(IConfiguration config, ICaseEvent caseEvent)
        {
            _sqlConnStr = config.GetConnectionString("Default");
            _caseEventService = caseEvent;
        }
        public async Task<Case> LoadCase(int _caseID)
        {

            var procedure = "Case_GetOne";
            var _params = new DynamicParameters();

            _params.Add(name: "@caseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: _caseID);


            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn.QueryAsync<Case>(procedure, _params, commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
               
            }
        }

        public async Task<Case> LoadCase(int _caseID, int contactId)
        {

            var procedure = "Case_GetOne_ByUser";
            var _params = new DynamicParameters();

            _params.Add(name: "@caseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: _caseID);
            _params.Add(name: "@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: contactId);


            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn.QueryAsync<Case>(procedure, _params, commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
               
            }

        }

        public async Task<List<CaseModel.CaseEventStatus>> GetStatusByCase(int caseID, string caseRole)
        {

            var procedure = "Case_GetEventByStatus";
            var _params = new DynamicParameters();

            _params.Add(name: "@caseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseID);
            _params.Add(name: "@caseRole", dbType: DbType.String, direction: ParameterDirection.Input, value: caseRole);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn.QueryAsync<CaseModel.CaseEventStatus>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<CaseModel.CaseEventStatus> _CaseStatusList = result.ToList<CaseModel.CaseEventStatus>();
                return _CaseStatusList;
            }

        }

        public async Task<List<CaseEvent>> GetEventList(int caseID)
        {
            var procedure = "CaseEvent_GetAll";
            var _params = new DynamicParameters();

            _params.Add(name: "@caseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseID);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                var result = await conn.QueryAsync<CaseEvent>(procedure, _params, commandType: CommandType.StoredProcedure);

                List<CaseEvent> _CaseEvent = result.ToList<CaseEvent>();

                return _CaseEvent;
            }

        }

        public async Task OnEvent(string eventID, int IssuedByID, int caseID, XmlDocument xmlData, string EventText, List<Contact> NotifyList)
        {
            CaseEvent cE = new CaseEvent();
            cE.caseID = caseID;
            cE.eventID = eventID;
            cE.IssuedByID = IssuedByID;
            cE.CaseEventData = xmlData.InnerXml;
            cE.CaseEventText = EventText;
            cE.CaseEventNotifyContacts = NotifyList.ObjToXml("ContactList").ToString();

            await _caseEventService.Create(cE);
        }

        public async Task ChangeParticipants(int caseID, XmlDocument xmlParticipants)
        {
            var procedure = "Case_UpdateParticipants";
            var _params = new DynamicParameters();

            _params.Add(name: "@caseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseID);
            _params.Add(name: "@Participants", dbType: DbType.Xml, direction: ParameterDirection.Input, value: xmlParticipants.InnerXml);

            using (var conn = new SqlConnection(_sqlConnStr))
            {
                await conn.ExecuteAsync(procedure, _params, commandType: CommandType.StoredProcedure);                
            }
        }
       
    }
}
