using System;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace Common
{
    public class SystemSettings
    {
        #region System settings
        public static DataTable Retrive()
        {

            // Create parameter array
            SqlParameter[] parameters =
			{
				new SqlParameter( "@ReturnValue",	SqlDbType.Int )			// 0
			};

            // Set parameter values and directions
            parameters[0].Direction = ParameterDirection.ReturnValue;

            DataTable retTable;
            retTable = DAO.ExecSqlProcedureReturnDT("dbo.AppSettings_Get", parameters);

            switch ((int)parameters[0].Value)
            {
                case 0:
                    {
                        return retTable;
                    }
                default:
                    {
                        throw new Exception("Retrive.UndefinedError");
                    }
            }
        }
        #endregion

        #region Delta next run time
        public static bool DeltaNextRunTime(out string _hour, out string _min)
        {
            _hour = "11";
            _min = "0";

            SqlParameter[] parаms = new SqlParameter[]
			{
				new SqlParameter("@DBSstartTimeHH",	SqlDbType.Int),
				new SqlParameter("@DBSstartTimeMM",		SqlDbType.Int ),
				new SqlParameter("@ReturnValue",	SqlDbType.Int )					
			};

            parаms[0].Direction = ParameterDirection.Output;
            parаms[1].Direction = ParameterDirection.Output;
            parаms[2].Direction = ParameterDirection.ReturnValue;

            DAO.ExecSqlProcedure("pAppSettings_Retrive_DBSstartTime", parаms);

            switch ((int)parаms[2].Value)
            {
                case 0:
                    {
                        _hour = Convert.ToString(parаms[0].Value);
                        _min = Convert.ToString(parаms[1].Value);
                        return true;
                    }
                default:
                    {
                        throw new Exception("DeltaNextRunTime.UndefinedError");
                    }
            }

        }
        #endregion



        #region email queue
        public static DataTable MailQueueRetrive(int top)
        {

            SqlParameter[] parameters =
			{
				new SqlParameter( "@Top",	SqlDbType.Int ),			// 0
				new SqlParameter( "@ReturnValue",	SqlDbType.Int )			// 1
			};

            parameters[0].Value = top;
            parameters[1].Direction = ParameterDirection.ReturnValue;

            DataTable retTable = DAO.ExecSqlProcedureReturnDT("pMailQueue_Retrive", parameters);

            switch ((int)parameters[1].Value)
            {
                case 0:
                    {
                        return retTable;
                    }
                default:
                    {
                        throw new Exception("MailQueueRetrive.UndefinedError");
                    }
            }
        }

        public static void MailQueueUpdate(int recId, int StatusId)
        {

            SqlParameter[] parameters =
			{
				new SqlParameter( "@recId",		SqlDbType.Int ),	// 0
				new SqlParameter( "@StatusId",		SqlDbType.Int ),	// 1
				new SqlParameter( "@ReturnValue",	SqlDbType.Int )		// 2
			};

            parameters[0].Value = recId;
            parameters[1].Value = StatusId;
            parameters[2].Direction = ParameterDirection.ReturnValue;

            DAO.ExecSqlProcedure("pMailQueue_Update", parameters);

        }

        public static void MailQueueInsert(int TypeID, string FormAddr, string ToAddr, string Bcc, string Subj, string Body, string FileName)
        {

            SqlParameter[] parameters =
			{
				new SqlParameter( "@TypeID",		SqlDbType.Int ),	// 0
				new SqlParameter( "@FormAddr",		SqlDbType.VarChar, 150 ),	// 1
				new SqlParameter( "@ToAddr",		SqlDbType.VarChar, 150 ),	// 2
				new SqlParameter( "@Bcc",		SqlDbType.VarChar, 150 ),	// 3
				new SqlParameter( "@Subj",		SqlDbType.NVarChar, 150 ),	// 4
				new SqlParameter( "@Body",		SqlDbType.NText ),	// 5
				new SqlParameter( "@FileName",		SqlDbType.NVarChar, 255 ),	// 6
				new SqlParameter( "@ReturnValue",	SqlDbType.Int )		// 7
			};

            parameters[0].Value = TypeID;
            parameters[1].Value = FormAddr.Trim();
            parameters[2].Value = ToAddr.Trim();
            parameters[3].Value = Bcc.Trim();
            parameters[4].Value = Subj.Trim();
            parameters[5].Value = Body.Trim();
            parameters[6].Value = FileName.Trim();
            parameters[7].Direction = ParameterDirection.ReturnValue;

            DAO.ExecSqlProcedure("pMailQueue_Insert", parameters);

        }
        #endregion

        
    }
}
