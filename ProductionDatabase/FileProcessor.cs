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
        public static void Process_File(string file_full_name, string connection_string)
        {
            if (File.Exists(file_full_name))
            {
                string filename = Path.GetFileName(file_full_name);
                string error_dir = Path.GetDirectoryName(file_full_name);
                string archive_dir = Path.GetDirectoryName(file_full_name);
                error_dir = Path.Combine(error_dir, "error");
                archive_dir = Path.Combine(archive_dir, "archive");

                string[] sFilename = { };
                try
                {
                    sFilename = filename.Split('_');
                }
                catch
                {
                    /* methodfullname */
                    MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    string methodName = method.Name;
                    string className = method.ReflectedType.Name;
                    string fullMethodName = className + "." + methodName;

                    Library.Logger.Log_Error(fullMethodName, "Couldnot split filename using '_'. Filename: " + filename);
                    if (!Directory.Exists(error_dir)) { Directory.CreateDirectory(error_dir); }
                    File.Move(file_full_name, Path.Combine(error_dir, filename));
                }

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

                            if (!Directory.Exists(archive_dir))
                            {
                                Directory.CreateDirectory(archive_dir);
                            }
                            File.Move(file_full_name, Path.Combine(archive_dir, Path.GetFileName(file_full_name)));

                            if (Error.Count > 0)
                            {
                                if (!Directory.Exists(error_dir))
                                {
                                    Directory.CreateDirectory(error_dir);
                                }
                                File.AppendAllText(
                                    Path.Combine(error_dir, "Share_" + DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm_ss_fff") + ".txt")
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

                            if (!Directory.Exists(error_dir)) { Directory.CreateDirectory(error_dir); }
                            File.Move(file_full_name, Path.Combine(error_dir, filename));
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

                        if (!Directory.Exists(error_dir)) { Directory.CreateDirectory(error_dir); }
                        File.Move(file_full_name, Path.Combine(error_dir, filename));
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

                    if (!Directory.Exists(error_dir)) { Directory.CreateDirectory(error_dir); }
                    File.Move(file_full_name, Path.Combine(error_dir, filename));
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
