using AustinsFirstProject.AlphaVantage;
using AustinsFirstProject.Library;
using AustinsFirstProject.Library.DatabaseTable;
using AustinsFirstProject.Library.Intrinio;
using Library.Stock;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AustinsFirstProject.Tester
{
    public static class Tester
    {
        public static void Test_Share_Insert_Update()
        {
            List<Ticker_Class> tk = null;
            string result = "Before TRY.";

            tk = Library.Intrinio.Utility.Database.Get_Tickers();
            string apikey = "9BTE2MLIE1VPTO4I";
            result = TIME_SERIES_DAILY.GET(tk[0].Ticker, apikey);

            List<Share> shares = JsonConvert.DeserializeObject<List<Share>>(result);

            object save_in_db_tracker;
            for (int i = 0; i < shares.Count; i++)
            {
                save_in_db_tracker = shares[i].Save_in_Database();
                Console.WriteLine("Share successfully processed for date: " + shares[i]._date + ". Result: " + save_in_db_tracker.ToString());
                
            }
            Console.ReadLine();
        }
        
    }
}
