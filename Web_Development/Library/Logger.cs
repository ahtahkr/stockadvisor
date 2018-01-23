using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AustinsFirstProject.Library
{
    public static class Logger
    {
        public static void Log_Error(string message, string _filename = "Error_")
        {
            string filename = _filename + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".log";
            string base_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            
            if (!Directory.Exists(base_directory))
            {
                Directory.CreateDirectory(base_directory);
            }

            try
            {
                File.AppendAllText(
                     Path.Combine(base_directory, filename)
                    , DateTime.Now.ToString("yyyy_MM_dd_HH:mm:ss") + " : " + message + Environment.NewLine
                    );
            } catch (Exception ex)
            {
                Logger.Log("error log failed: " + ex.Message);
            }
        }
        public static void Log(string message, string _filename = "", string folder = "")
        {
            string base_directory;
            string filename;

            if (!String.IsNullOrEmpty(folder))
            {
                base_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", folder);
            } else
            {
                base_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            }

            if (!String.IsNullOrEmpty(_filename))
            {
                filename = _filename + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".log";
            } else
            {
                filename = "Log_" + DateTime.Now.ToString("yyyy_MM_dd") + ".log";
            }

            if (!Directory.Exists(base_directory))
            {
                Directory.CreateDirectory(base_directory);
            }

            File.AppendAllText(
                 Path.Combine(base_directory, filename)
                , DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + " : " + message + Environment.NewLine
                );
        }
    }
}
