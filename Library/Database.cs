using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Library.Database
{
    public class SP_Exec_Result
    {
        public string Result { get; set; }
    }
    public static class ExecuteProcedure
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
            {
                result.Add(col, reader[col]);
            }
            return result;
        }

        public static string Get(string commandName, string connection_string, Dictionary<string, object> parameters = null)
        {
            SqlConnection conn = new SqlConnection(connection_string);

            try
            {
                conn.Open();
            }
            catch (Exception ex)
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
            string result = JsonConvert.SerializeObject(Serialize(comm.ExecuteReader()));
            conn.Close();
            conn.Dispose();
            SqlConnection.ClearPool(conn);
            return result;
        }
    }
}