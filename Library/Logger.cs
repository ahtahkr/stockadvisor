using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library
{
    public static class Logger
    {
        public static void Log_Error(string method, string message, string exception_message = "No Message")
        {
            string filename = "Error_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") + ".error";
            string base_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "Error");
            
            if (!Directory.Exists(base_directory))
            {
                Directory.CreateDirectory(base_directory);
            }

            string body  = "Date: " + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
            body += Environment.NewLine + Environment.NewLine;
            body += "Method: " + method;
            body += Environment.NewLine + Environment.NewLine;
            body += "Message: " + message;
            body += Environment.NewLine + Environment.NewLine;
            body += "Exception Message: " + exception_message;

            File.AppendAllText(
                 Path.Combine(base_directory, filename)
                , body
            );
        }

        public static void Log(string message, string _filename = "", string folder = "")
        {
            string base_directory;
            string filename;

            if (!String.IsNullOrEmpty(folder))
            {
                base_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", folder);
            }
            else
            {
                base_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            }

            if (!String.IsNullOrEmpty(_filename))
            {
                filename = _filename + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".log";
            }
            else
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
