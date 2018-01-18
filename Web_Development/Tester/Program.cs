using AustinsFirstProject.Library;
using AustinsFirstProject.Library.Intrinio;
using Library.Stock;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AustinsFirstProject.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Ticker_Class> tk = Library.Intrinio.Utility.Database.Get_Tickers();

            string url;
            string result;

            string batch = "";
            int length = 5;
            for (int a = 0; a < tk.Count; a++)
            {
                if (a < length) { batch += tk[a].Ticker; }
                if (a < (length - 1)) //(tk.Count - 1))
                {
                    batch += ",";
                }
            }

            url = "https://www.alphavantage.co/query?function=BATCH_STOCK_QUOTES&symbols=" + batch + "&apikey=9BTE2MLIE1VPTO4I";
            result = HttpRequestUtility.GetRequest(url);
            Logger.Log_Error(result);
        }

        private static void Get_Companies()
        {
            Console.WriteLine("Retrieving Companies from 'api.intrinio.com'.");

            string url = "https://api.intrinio.com/companies?page_number=";
            string username = "7d11f2289bbb035fc56c6ff5b654e6bd";
            string password = "6c9a7fdd82022fcea63c2367a117750b";

            int a = 1;
            string result = HttpRequestUtility.GetRequest(url + a, username, password);

            Companies _companies = JsonConvert.DeserializeObject<Companies>(result);
            _companies.Save_in_Database();

            int total_pages = _companies.total_pages;

            for (a = 2; a <= total_pages; a++)
            {
                result = HttpRequestUtility.GetRequest(url + a, username, password);
                _companies = JsonConvert.DeserializeObject<Companies>(result);
                _companies.Save_in_Database();
            }

            Console.WriteLine("Complete.");
            Console.ReadLine();
        }
    }
}
