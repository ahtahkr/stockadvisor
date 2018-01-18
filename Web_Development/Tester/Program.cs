using AustinFirstProject.Library;
using AustinFirstProject.Library.Intrinio;
using Newtonsoft.Json;
using System;

namespace AustinFirstProject.Tester
{
    class Program
    {

        static void Main(string[] args)
        {
            
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
