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
    [Serializable]
    [DataContract(Namespace = "DominoCMS")]
    public class CaseEvent:ICaseEvent
    {
        public int CaseEventID { get; set; }
        public int caseID { get; set; }
        public string eventID { get; set; }
        public string eventName { get; set; }
        public int IssuedByID { get; set; }
        public string ContactName { get; set; }
        public DateTime CreateDate { get; set; }
        public string CaseEventData { get; set; }

        public string CaseEventNotifyContacts { get; set; }
        public string CaseEventText { get; set; }
        public Boolean AddReport { get; set; }

        public readonly IConfiguration _config;

        public CaseEvent(IConfiguration config)
        {
            _config = config;
        }
        public CaseEvent()
        {
            CaseEventID = 0;
            caseID = 0;
            CaseEventText = "";
            CaseEventData = ""; //new XmlDocument();
            CaseEventNotifyContacts = ""; //new CaseContactList();
        }

        public async Task<List<CaseEvent>> GetHistoryByCase(int CaseID)
        {
            var procedure = "CaseEvent_GetAll";
            var _params = new DynamicParameters();

            _params.Add(name: "@caseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseID);

            try
            {
                using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    var result = await conn.QueryAsync<CaseEvent>(procedure, _params, commandType: CommandType.StoredProcedure);

                    List<CaseEvent> _CaseEvent = result.ToList<CaseEvent>();
                    
                    return _CaseEvent;
                }
            }
            catch //(Exception ex)
            {
                return null;
            }

            //SqlParameter[] parms = new SqlParameter[] {
            //               new SqlParameter("@caseID", SqlDbType.Int)
            //};
            //parms[0].Value = CaseID;
            //return DAO.ExecSqlProcedureReturnDt("CaseEvent_GetAll", parms);
        }

        public async Task<int> Create()
        {

            var procedure = "CaseEvent_Create";
            var _params = new DynamicParameters();

            _params.Add(name: "@caseID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: caseID);
            _params.Add(name: "@eventID", dbType: DbType.String, direction: ParameterDirection.Input, value: eventID);
            _params.Add(name: "@IssuedByID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: IssuedByID);
            _params.Add(name: "@CaseEventData", dbType: DbType.Xml, direction: ParameterDirection.Input, value: CaseEventData);
            _params.Add(name: "@CaseEventNotifyContacts", dbType: DbType.Xml, direction: ParameterDirection.Input, value: CaseEventNotifyContacts);
            _params.Add(name: "@CaseEventText", dbType: DbType.String, direction: ParameterDirection.Input, value: CaseEventText);


            //_params.Add(name: "@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            try
            {
                 using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {

                    var result = await conn.QueryAsync<CaseEvent>(procedure, _params, commandType: CommandType.StoredProcedure);

                    CaseEvent _CaseEvent = result.First();

                    if (_CaseEvent.CaseEventID > 0)
                        CaseEventHandlers.OnEvent(this);

                    return 1;

                }
            }
            catch //(Exception ex)
            {
                return -1;
            }
            

        }

        public bool SendMailToNotifyList()
        {

            var procedure = "CaseEvent_SendMailToNotifyList";
            var _params = new DynamicParameters();

            _params.Add(name: "@CaseEventID", dbType: DbType.Int32, direction: ParameterDirection.Input, value: CaseEventID);
            _params.Add(name: "@CaseEventNotifyContacts", dbType: DbType.Xml, direction: ParameterDirection.Input, value: CaseEventNotifyContacts);

            try
            {
                 using (var conn = new SqlConnection(_config.GetConnectionString("Default")))
                {

                    var result = conn.ExecuteScalar(procedure, _params, commandType: CommandType.StoredProcedure);

                    int RetID = 0;
                    try
                    {
                        RetID = Convert.ToInt32(result);
                    }
                    catch{}

                    if (RetID == 1)
                        return true;
                    else
                        return false;

                }
            }
            catch
            {
                return false;
            }
            

        }

        public IConfiguration GetIConfiguration()
        {
            return _config;
        }

        //public  bool  SendMailToNotifyList()
        //{
        //    SqlParameter[] parms = new SqlParameter[] {
        //                   new SqlParameter("@CaseEventID", SqlDbType.Int),
        //                   new SqlParameter("@CaseEventNotifyContacts", SqlDbType.Xml)
                            
        //    };
        //    parms[0].Value = CaseEventID;
        //    parms[1].Value = CaseEventNotifyContacts.AsXml().InnerXml;


        //    object o = DAO.ExecuteScalar(CommandType.StoredProcedure, "CaseEvent_SendMailToNotifyList", parms);
        //    int RetID = 0;
        //    try
        //    {
        //        RetID = Convert.ToInt32(o);
        //    }
        //    catch{}

        //    if (RetID == 1)
        //        return true;
        //    else
        //        return false;

        //}
    }

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
        //public CMSEvent(string eventID)
        //{
        //    Load(eventID);
        //}

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
