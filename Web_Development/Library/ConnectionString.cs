using System;
using System.Configuration;

namespace AustinFirstProject.Library
{
    static class ConnectionString
    {
        public static string Get()
        {
            string connection_string;

            //connection_string = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["environment"]].ConnectionString;
            connection_string = "Data Source=AADHIKARI10\\SQLEXPRESS;Initial Catalog=austin_stock_processor;User ID=developer;Password=developer";

            return (String.IsNullOrEmpty(connection_string) ? "" : connection_string);
        }
    }
}
