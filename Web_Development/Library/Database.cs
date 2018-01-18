using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AustinsFirstProject.Library
{
    public static class Database
    {
        private static IEnumerable<Dictionary<string, object>> Serialize(SqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
                cols.Add(reader.GetName(i));

            while (reader.Read())
                results.Add(SerializeRow(cols, reader));

            return results;
        }
        private static Dictionary<string, object> SerializeRow(IEnumerable<string> cols,
                                                        SqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }

        public static string ExecuteProcedure_Get(string commandName, Dictionary<string, object> parameters)
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
            return JsonConvert.SerializeObject(Serialize(comm.ExecuteReader(CommandBehavior.CloseConnection)));
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
