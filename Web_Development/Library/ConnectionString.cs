using System;
using System.Configuration;

namespace AustinFirstProject.Library
{
    static class ConnectionString
    {
        public static string Get()
        {
            string connection_string = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["environment"]].ConnectionString;
            return (String.IsNullOrEmpty(connection_string) ? "" : connection_string);
        }
    }
}
