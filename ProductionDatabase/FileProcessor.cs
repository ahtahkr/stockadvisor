using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ProductionDatabase
{
    public static class FileProcessor
    {
        public static void Process_File(string file_full_name, string connection_string, string error_folder)
        {
            if (File.Exists(file_full_name))
            {
                string filename = Path.GetFileName(file_full_name);
                string[] sFilename = filename.Split('_');
                if (sFilename.Length > 1)
                {
                    if (sFilename[0].ToLower().Equals("share"))
                    {
                        List<Share> Error = new List<Share>();
                        string text = File.ReadAllText(file_full_name);

                        try
                        {
                            List<Share> Shares = JsonConvert.DeserializeObject<List<Share>>(text);
                            for (int a = 0; a < Shares.Count; a++)
                            {
                                if (Shares[a].Save_in_Database(connection_string))
                                { }
                                else
                                {
                                    Error.Add(Shares[a]);
                                }
                            }

                            if (Error.Count > 0)
                            {
                                if (!Directory.Exists(error_folder))
                                {
                                    Directory.CreateDirectory(error_folder);
                                }
                                File.AppendAllText(
                                    Path.Combine(error_folder, "Share_" + DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm_ss_fff") + ".txt")
                                    , JsonConvert.SerializeObject(Error));
                            }
                        } catch (Exception ex)
                        {
                            /* staticmethodfullname */
                            MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                            string methodName = method.Name;
                            string className = method.ReflectedType.Name;
                            string fullMethodName = className + "." + methodName;
                            Library.Logger.Log_Error(fullMethodName, "File Content: " + text, ex.Message);
                        }
                    }
                    else
                    {
                        /* methodfullname */
                        MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                        string methodName = method.Name;
                        string className = method.ReflectedType.Name;
                        string fullMethodName = className + "." + methodName;

                        Library.Logger.Log_Error(fullMethodName, "'" + string.Join(",", sFilename) + "'. The first word of " + file_full_name + " is invalid.");
                    }
                }
                else
                {
                    /* methodfullname */
                    MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    string methodName = method.Name;
                    string className = method.ReflectedType.Name;
                    string fullMethodName = className + "." + methodName;

                    Library.Logger.Log_Error(fullMethodName, "'" + string.Join(",", sFilename) + "' is invalid. Filename: " + file_full_name);
                }
            }
            else
            {
                /* methodfullname */
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                string fullMethodName = className + "." + methodName;

                Library.Logger.Log_Error(fullMethodName, "'" + file_full_name + "' does not exists.");
            }
        }
    }
}
