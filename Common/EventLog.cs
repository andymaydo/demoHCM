using System;
using System.Data;
using System.Data.SqlClient;
using Common;
namespace Common
{
    public class EventLog
    {
        public static void Insert(int EvCode, string EvDescr)
        {
  
            SqlParameter[] parameters =
			{
				new SqlParameter( "@EvCode",	SqlDbType.Int ),		// 0
				new SqlParameter( "@ErrMessage",	SqlDbType.NVarChar, 4000 ),		// 1
				new SqlParameter( "@ReturnValue",	SqlDbType.Int )		// 2
			};

            parameters[0].Value = EvCode;
            parameters[1].Value = EvDescr;
            parameters[2].Direction = ParameterDirection.ReturnValue;


            try
            {
                DAO.ExecSqlProcedure("pErrLog_Create", parameters);                
            }
            catch
            { };
        }
    }
}
