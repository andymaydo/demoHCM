using Microsoft.Extensions.Configuration;
using System;

namespace HCMDataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public string ConnStrName { get; set; } = "Default";
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public string GetConnStrName()
        {
            return _config.GetConnectionString(ConnStrName);
        }


    }
}
