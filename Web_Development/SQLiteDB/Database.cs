using AustinsFirstProject.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using Microsoft.Data.Sqlite;

namespace AustinsFirstProject.SQLiteDB
{
    public static class Database
    {
        public static string DBNAME = "SQLITEDB.db3";
        public static string DBDIR = Path.Combine(Utility.Get_Directory(), "Database");
        public static string DBFILE = Path.Combine(DBDIR, DBNAME);

        public static string Get_Connection_String()
        {
            if (!File.Exists(DBFILE))
            {
                if (!Directory.Exists(DBDIR))
                {
                    Directory.CreateDirectory(DBDIR);
                }

                FileStream fs = File.Create(DBFILE);
                fs.Close();
            }
            return DBFILE;
        }

        public static List<Dictionary<string, string>> GetNameUrl()
        {
            List<Dictionary<string,string>> ImportedFiles = new List<Dictionary<string, string>>();
            string connection_string = "Data Source=" + Get_Connection_String() + ";";

            try
            {
                using (SqliteConnection connect = new SqliteConnection(connection_string))
                {
                    connect.Open();
                    using (SqliteCommand fmd = connect.CreateCommand())
                    {
                        fmd.CommandText = @"SELECT name, url FROM git_repos";
                        fmd.CommandType = CommandType.Text;
                        SqliteDataReader r = fmd.ExecuteReader();
                        while (r.Read())
                        {
                            Dictionary<string, string> dic = new Dictionary<string, string>();
                            dic.Add("name", r["name"].ToString());
                            dic.Add("url", r["url"].ToString());
                            ImportedFiles.Add(dic);
                        }
                    }
                }
            } catch (Exception ex)
            {
                Logger.Log_Error("Sqlite GetNameUrl failed. " + connection_string + " Error Message: " + ex.Message, "Sqlite");
            }
            return ImportedFiles;
        }


        public static Return Create_Database()
        {
            try {

                if (File.Exists(Path.Combine(Utility.Get_Directory(), "Database", DBNAME)))
                {
                    return new Return(1, "Database already exists.");
                }
                string connection_string = "Data Source='" + Get_Connection_String() + "';";
                //Logger.Log_Error("Create Sqlite Database connection_string: " + connection_string, "Sqlite");
                
                SqliteConnection m_dbConnection = new SqliteConnection(connection_string);
                m_dbConnection.Open();

                Execute_Query("create table git_repos (name varchar(20), url varchar(500))");
                Execute_Query("insert into git_repos (name, url) values ('Name_Test', 'Url_Test')");

                return new Return(0, m_dbConnection.ConnectionString);
                
            } catch (Exception ex)
            {
                Logger.Log_Error("Create Sqlite Database failed. Error Message: " + JsonConvert.SerializeObject(ex), "Sqlite");
            }
            return new Return(1, "");
        }

        public static Return Execute_Query(string query)
        {
            try
            {
                SqliteConnection m_dbConnection = new SqliteConnection("Data Source=" + Get_Connection_String() + ";");
                m_dbConnection.Open();

                SqliteCommand command = new SqliteCommand(query, m_dbConnection);
                command.ExecuteNonQuery();


                //Logger.Log_Error("Execute Query: " + query, "Sqlite");

                return new Return(0, "");

            }
            catch (Exception ex)
            {
                Logger.Log_Error("Sqlite, Execute_Query, failed. Error Message: " + ex.Message, "Sqlite");
            }
            return new Return(1, "");
        }



        /*
        public static Return Create_table(string table_name, Dictionary<string, string> parameters)
        {
            string sql = "";
            string keys = "";
            string values = "";
            try
            {
                foreach (KeyValuePair<string, string> kvp in parameters)
                {
                    keys += kvp.Key;
                    values += kvp.Value;
                }
            } catch (Exception ex)
            {
                Logger.Log_Error("Create Sqlite Database failed. sqlquery: " + sql + " Error Message: " + ex.Message, "Sqlite");
            }
            return new Return(1, "");
        }
        */





    }
}
