using Library.Stock;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AustinsFirstProject.Library.Intrinio
{
    public static class Utility
    {
        public static class Database
        {
            public static List<Ticker_Class> Get_Tickers()
            {
                string tickers = Library.Database.ExecuteProcedure_Get("[dbo].[Company_Get_Unique_Ticker]", new Dictionary<string, object>());

                List<Ticker_Class> tk = JsonConvert.DeserializeObject<List<Ticker_Class>>(tickers);

                return tk;
            }
        }
    }
}
