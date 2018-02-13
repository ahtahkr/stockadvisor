using AustinsFirstProject.Library;
using Library.Stock;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AustinsFirstProject.StockAdvisor.IEXTrading
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

            public static string Symbols()
            {
                return HttpRequestUtility.GetRequest(URL + "ref-data/symbols");
            }

            public static string Chart(string _symbol, string range = "")
            {
                if (String.IsNullOrEmpty(range))
                {
                    return HttpRequestUtility.GetRequest(URL + "stock/" + _symbol + "/chart");
                } else
                {
                    return HttpRequestUtility.GetRequest(URL + "stock/" + _symbol + "/chart/" + range );
                }

                /*
                 
                 Options
Range	Description	Source
5y	Five years	Historically adjusted market-wide data
2y	Two years	Historically adjusted market-wide data
1y	One year	Historically adjusted market-wide data
ytd	Year-to-date	Historically adjusted market-wide data
6m	Six months	Historically adjusted market-wide data
3m	Three months	Historically adjusted market-wide data
1m	One month (default)	Historically adjusted market-wide data
1d	One day	IEX-only data by minute
date	Specific date	IEX-only data by minute for a specified date in the format YYYYMMDD if available. Currently supporting trailing 30 calendar days.
dynamic	One day	Will return 1d or 1m data depending on the day or week and time of day. Intraday per minute data is only returned during market hours.

                 */
            }
        }

        public static string Get_Full_FileName_to_Save_Api_Result(string directory, string identity = "")
        {
            if (String.IsNullOrEmpty(directory))
            {
                directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IEXTrading", "Api");
            }

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (String.IsNullOrEmpty(identity))
            {
            }
            else
            {
                identity += "_";
            }

            return Path.Combine(directory, identity + DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm_ss_fffffff_K") + ".txt");
        }
    }
}
