using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
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

        /// <summary>
        /// This method assumes that the stored procedure returns [{"Result":0}] i.e. 'Select 0 as Result' when successful and 'Select 1 as Result' when unsuccessful.
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="connection_string"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static bool Post(string commandName, string connection_string, Dictionary<string, object> parameters = null)
        {
            SqlConnection conn = new SqlConnection(connection_string);

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                /* staticmethodfullname */
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                string fullMethodName = className + "." + methodName;
                Logger.Log_Error(fullMethodName, "Connection to the database failed.", ex.Message);
                return false;
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
            try
            {
                List<SP_Exec_Result> sP_Exec_Result = JsonConvert.DeserializeObject<List<SP_Exec_Result>>(result);
                if (sP_Exec_Result[0].Result == "0")
                {
                    return true;
                }
                else if (sP_Exec_Result[0].Result == "1") { return false; }
                else
                {
                    /* staticmethodfullname */
                    MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    string methodName = method.Name;
                    string className = method.ReflectedType.Name;
                    string fullMethodName = className + "." + methodName;
                    Logger.Log_Error(fullMethodName, "SP returned invalid value: " + result);
                    return false;
                }
            } catch (Exception ex)
            {
                /* staticmethodfullname */
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                string fullMethodName = className + "." + methodName;
                Logger.Log_Error(fullMethodName, "Connection to the database failed.", ex.Message);
                
                return false;
            }
        }
    }
}