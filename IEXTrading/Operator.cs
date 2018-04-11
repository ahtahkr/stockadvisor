using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace IEXTrading
{
    public enum Web_Api_Version
    {
        One_point_Zero
    }
    public static class Operator
    {
        public static void Save_Previous_to_File(Web_Api_Version web_api_version, string folder, string symbol = "market")
        {
            string filename = symbol + "_" + DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm_ss") + ".txt";
            folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            folder = Path.Combine(folder, filename);
            string previous = "Before";

            if (web_api_version == Web_Api_Version.One_point_Zero)
            {
                previous = WebApi.Api_1.Previous(symbol);
            } else
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                string fullMethodName = className + "." + methodName;

                Library.Logger.Log_Error(fullMethodName, "The web_api_version received: " + web_api_version);

                return;
            }           

            File.AppendAllText(
                 folder
                , previous
            );
        }

        public static void Save_Chart_Range_to_File(Web_Api_Version web_api_version, string folder, string symbol, string range)
        {
            string filename = symbol + "_" + DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm_ss") + ".txt";
            folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            folder = Path.Combine(folder, filename);
            string previous = "Before";

            if (web_api_version == Web_Api_Version.One_point_Zero)
            {
                previous = WebApi.Api_1.Chart(symbol, range);
            }
            else
            {
                MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                string methodName = method.Name;
                string className = method.ReflectedType.Name;
                string fullMethodName = className + "." + methodName;

                Library.Logger.Log_Error(fullMethodName, "The web_api_version received: " + web_api_version);

                return;
            }

            File.AppendAllText(
                 folder
                , previous
            );
        }
    }
}
