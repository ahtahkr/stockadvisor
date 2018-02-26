using Library.Stock;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Library.Intrinio
{
    public static class Utility
    {

        public static class Database
        {
            public static List<Ticker_Class> Get_Tickers()
            {
                string tickers = "";
                try
                {
                    tickers = Library.Database.ExecuteProcedure_Get("[dbo].[Company_Get_Unique_Ticker]", new Dictionary<string, object>());

                    if ( String.IsNullOrEmpty(tickers) || tickers.Contains("\"ticker\":\"null\""))
                    {
                        return new List<Ticker_Class>();
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<List<Ticker_Class>>(tickers);
                    }
                } catch (Exception ex)
                {
                    Logger.Log_Error("tickers: " + tickers + ". Get_Tickers failed. Message: " + ex.Message);
                    return new List<Ticker_Class>();
                }
                
            }
        }
    }
}
