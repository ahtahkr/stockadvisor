using AustinsFirstProject.Library;
using Library.Stock;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AustinsFirstProject.StockProcessor.IEXTrading
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

                    if (String.IsNullOrEmpty(tickers) || tickers.Contains("\"ticker\":\"null\""))
                    {
                        return new List<Ticker_Class>();
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<List<Ticker_Class>>(tickers);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log_Error("tickers: " + tickers + ". Get_Tickers failed. Message: " + ex.Message);
                    return new List<Ticker_Class>();
                }

            }
        }

        public static class HttpRequestor
        {
            public const string URL = "https://api.iextrading.com/1.0/";

            public static string Previous(string _symbol)
            {
                return HttpRequestUtility.GetRequest(URL + "stock/" + _symbol + "/previous");
            }
            public static string OHLC(string _symbol)
            {
                return HttpRequestUtility.GetRequest(URL + "stock/" + _symbol + "/ohlc");
            }
        }

        public static string Get_Full_FileName_to_Save_Api_Result(string directory)
        {
            if (String.IsNullOrEmpty(directory))
            {
                directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IEXTrading", "Api");
            }

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return Path.Combine(directory, DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm_ss_ffff") + ".txt");
        }
    }
}
