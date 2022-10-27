using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace DbRepo
{
    public class LookUpTableService : ILookUpTables
    {
        private List<CrossStatusRoleEvent> _events;
        private string _sqlConnStr;

        public List<CrossStatusRoleEvent> StatusRoleEventsList { 
            get { 
                return _events; 
            }
        }

        public LookUpTableService(IConfiguration config)
        {
            _sqlConnStr = config.GetConnectionString("Default");
            _events = LoadCrossStatusEvent();
        }

        private List<CrossStatusRoleEvent> LoadCrossStatusEvent()
        {
            string sql = "SELECT CaseStatusID , CaseRole, EventID FROM [dbo].[CrossStatusEvent] WHERE AppId=1";

            using (var connection = new SqlConnection(_sqlConnStr))
            {
                connection.Open();
                var result = connection.Query<CrossStatusRoleEvent>(sql, commandType: CommandType.Text);
                return (List<CrossStatusRoleEvent>)result;
            }
        }
    }
}
