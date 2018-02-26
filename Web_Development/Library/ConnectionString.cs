using System;
using System.Configuration;

namespace Library
{
    static class ConnectionString
    {
        public static string Get()
        {
            string connection_string;

            connection_string = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["environment"]].ConnectionString;
            //connection_string = "Data Source=AADHIKARI10\\SQLEXPRESS;Initial Catalog=austin_stock_processor;User ID=developer;Password=developer";
            //connection_string = "Data Source=DESKTOP-BJ0AH8G;Initial Catalog=austin_stock_processor;Integrated Security=True";

            return (String.IsNullOrEmpty(connection_string) ? "" : connection_string);
        }
    }
}
