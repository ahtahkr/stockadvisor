using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AustinStockAdvisor.Library
{
    public static class Logger
    {
        public static void Log_Error(string message, string _filename = "", string folder = "")
        {
            string base_directory;
            string filename;

            if (!String.IsNullOrEmpty(folder))
            {
                base_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "Error", folder);
            }
            else
            {
                base_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "Error");
            }

            if (!String.IsNullOrEmpty(_filename))
            {
                filename = _filename + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".error";
            }
            else
            {
                filename = "Error_" + DateTime.Now.ToString("yyyy_MM_dd") + ".error";
            }

            if (!Directory.Exists(base_directory))
            {
                Directory.CreateDirectory(base_directory);
            }

            File.AppendAllText(
                 Path.Combine(base_directory, filename)
                , DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss.fff") + " : " + message + Environment.NewLine
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
