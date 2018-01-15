using System;
using System.Configuration;

namespace ClassLibrary
{
    static class ConnectionString
    {
        public static string Gets()
        {
            string connection_string = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["environment"]].ConnectionString;
            return (String.IsNullOrEmpty(connection_string) ? "" : connection_string);
        }
    }
}
