using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AustinFirstProject.Library
{
    public static class Database
    {
        public static SqlDataReader ExecuteProcedure_Get(string commandName, Dictionary<string, object> parameters)
        { 
            SqlConnection conn = new SqlConnection(ConnectionString.Get());

            try
            {
                conn.Open();
            } catch (Exception ex)
            {
                Logger.Log("Connection to the database failed. Message: " + ex.Message);
                return null;
            }

            SqlCommand comm = conn.CreateCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = commandName;
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> kvp in parameters)
                    {
                    comm.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                }
            }
            return comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }


        public static int ExecuteProcedure_Post(string commandName, Dictionary<string, object> parameters)
        {
            SqlConnection conn = new SqlConnection(ConnectionString.Get());
            conn.Open();
            SqlCommand comm = conn.CreateCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = commandName;
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> kvp in parameters)
                {
                    comm.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                }
            }

            int a = 0;
            try
            {
                comm.ExecuteNonQuery();
            } catch (Exception ex) {
                a = -2;
                Logger.Log_Error("ExecuteProcedure_Post failed. Message: " + ex.Message);
            }
            conn.Close();

            return a;
        }
    }
}
