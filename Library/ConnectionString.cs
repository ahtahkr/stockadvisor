using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;


namespace personal_expenses.Library
{
    static class ConnectionString
    {
        public static string Get()
        {
            // string connection_string = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["environment"]].ConnectionString;
            // return (String.IsNullOrEmpty(connection_string) ? "" : connection_string);

            return "Data Source=DESKTOP-BJ0AH8G;Initial Catalog=personal_expenses;User ID=ashadhik;Password=123456;";
        }
    }
}
