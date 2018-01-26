using AustinsFirstProject.Library;
using AustinsFirstProject.AlphaVantage;
using AustinsFirstProject.Library.Intrinio;
using AustinsFirstProject.Library.DatabaseTable;
using Library.Stock;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using AustinsFirstProject.CoreLibrary.Database;

namespace AustinsFirstProject.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Get_All_Shares();
        }

        private static void Get_All_Shares()
        {
            Shares shares = new Shares();
            shares.Database_Connection_String = "Data Source=AADHIKARI10\\SQLEXPRESS;Initial Catalog=austin_stock_processor;Persist Security Info=True;User ID=developer;Password=developer";
            shares.Get_Ticker("VCOR");
            Console.WriteLine(shares.Shares_Date_Close());
            Console.ReadLine();
        }

        private static void Get_Companies()
        {
            //Console.WriteLine("Retrieving Companies from 'api.intrinio.com'.");

            //string url = "https://api.intrinio.com/companies?page_number=";
            //string username = "7d11f2289bbb035fc56c6ff5b654e6bd";
            //string password = "6c9a7fdd82022fcea63c2367a117750b";

            //int a = 1;
            //string result = HttpRequestUtility.GetRequest(url + a, username, password);

            //Companies _companies = JsonConvert.DeserializeObject<Companies>(result);
            //_companies.Save_in_Database();

            //int total_pages = _companies.total_pages;

            //for (a = 2; a <= total_pages; a++)
            //{
            //    result = HttpRequestUtility.GetRequest(url + a, username, password);
            //    _companies = JsonConvert.DeserializeObject<Companies>(result);
            //    _companies.Save_in_Database();
            //}

            //Console.WriteLine("Complete.");
            //Console.ReadLine();
        }
    }
}
