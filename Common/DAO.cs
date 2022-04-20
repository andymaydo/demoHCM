using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web;
using System.Xml;
using System.IO;
using System.Text;

namespace Common
{
    public abstract class DAO
    {
        public static string dbConnString;

   

        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }


        public static DataSet getDataSet(string sql)
        {
            SqlConnection conn = new SqlConnection(dbConnString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        public static DataSet getDataSet(string sql, string dsTableName)
        {
            SqlConnection conn = new SqlConnection(dbConnString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, conn);
            adapter.TableMappings.Add("Table", dsTableName);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        

        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(dbConnString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static object ExecuteScalar(string cmdText)
        {
            return DAO.ExecuteScalar(CommandType.Text, cmdText, null);
        }

        public static object ExecuteScalar(string cmdText, string ConnString)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, null);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        public static int ExecSqlProcedure(string procedure, params SqlParameter[] sparams)
        {
            using (SqlConnection conn = new SqlConnection(dbConnString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(procedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandTimeout
                    if (null != sparams)
                    {
                        for (int i = 0, len = sparams.Length; i < len; i++)
                        {
                            cmd.Parameters.Add(sparams[i]);
                        }
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataSet ExecSqlProcedureReturnDs(string procedure, params SqlParameter[] sparams)
        {
            using (SqlConnection conn = new SqlConnection(dbConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(procedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (null != sparams)
                    {
                        for (int i = 0, len = sparams.Length; i < len; i++)
                        {
                            cmd.Parameters.Add(sparams[i]);
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter((SqlCommand)cmd); ;
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            }
        }

        public static DataTable ExecSqlProcedureReturnDT(string procedure, params SqlParameter[] sparams)
        {
            using (SqlConnection conn = new SqlConnection(dbConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(procedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (null != sparams)
                    {
                        for (int i = 0, len = sparams.Length; i < len; i++)
                        {
                            cmd.Parameters.Add(sparams[i]);
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter((SqlCommand)cmd); ;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }



        public static XmlDocument ExecSqlXmlProcedure(string procedure, params SqlParameter[] sparams)
        {
            using (SqlConnection conn = new SqlConnection(dbConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(procedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (null != sparams)
                    {
                        for (int i = 0, len = sparams.Length; i < len; i++)
                        {
                            cmd.Parameters.Add(sparams[i]);
                        }
                    }

                    XmlDocument dom = new XmlDocument();
                    using (XmlReader reader = cmd.ExecuteXmlReader())
                    {
                        //XmlDocument dom = new XmlDocument();
                        dom.Load(reader);
                    }

                    return dom;


                }
            }
        }


    }
}
