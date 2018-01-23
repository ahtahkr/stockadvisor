using AustinsFirstProject.Library;
using AustinsFirstProject.AlphaVantage;
using AustinsFirstProject.Library.Intrinio;
using AustinsFirstProject.Library.DatabaseTable;
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
            string apikey = "9BTE2MLIE1VPTO4I";
            List<Ticker_Class> tk;

            tk = Library.Intrinio.Utility.Database.Get_Tickers();
            string result = TIME_SERIES_DAILY.GET(tk[0].Ticker, apikey);

           // Console.WriteLine(result);
           // Console.ReadLine();

            List<Share> shares = JsonConvert.DeserializeObject<List<Share>>(result);
            

            for (int i = 0; i < shares.Count; i++)
            {
                Console.WriteLine(shares[i].Save_in_Database());

                Console.ReadLine();
            }

            //Console.ReadLine();

            //while (true) //(!String.IsNullOrEmpty(result))
            //{
            //    tk = Library.Intrinio.Utility.Database.Get_Tickers();
            //    result = TIME_SERIES_DAILY.GET(tk[0].Ticker, apikey);
            //    Console.WriteLine(result);
            //};
            //result = HttpRequestUtility.GetRequest(url);
            //Logger.Log_Error(result);
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
