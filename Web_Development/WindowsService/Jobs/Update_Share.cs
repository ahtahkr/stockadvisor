﻿using AustinsFirstProject.AlphaVantage;
using AustinsFirstProject.Library.DatabaseTable;
using AustinsFirstProject.Library;
using Library.Stock;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;

namespace AustinsFirstProject.StockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private void Update_Share(object sender = null, ElapsedEventArgs e = null)
        {
            List<Ticker_Class> tk = null;
            string result = "Before TRY.";

            try
            {
                eventLog_i_am_active.WriteEntry(DateTime.Now + " : " + "AustinsFirstProject.StockAdvisor.WindowsService.Update_Shares");

                DateTime current_time = DateTime.Now;
                int current_hour_min = (current_time.Hour * 60) + current_time.Minute;
                
                int hour = Convert.ToInt32(ConfigurationManager.AppSettings["update_share_hour"]);
                int minute = Convert.ToInt32(ConfigurationManager.AppSettings["update_share_minute"]);
                string log = ConfigurationManager.AppSettings["log_update_share"];
                string apikey = ConfigurationManager.AppSettings["intrino_api_key"];

                int hour_min = (hour * 60) + minute;
                
                if (current_hour_min >= hour_min)
                {
                    tk = Library.Intrinio.Utility.Database.Get_Tickers();

                    if (tk.Count > 0)
                    {
                        List<string> date_list = new List<string>();
                        date_list.Add(DateTime.Now.ToString("yyyy-MM-dd"));
                        result = TIME_SERIES_DAILY.GET(tk[0].Ticker, apikey, date_list);

                        List<Share> shares = JsonConvert.DeserializeObject<List<Share>>(result);

                        object save_in_db_tracker;
                        for (int i = 0; i < shares.Count; i++)
                        {
                            save_in_db_tracker = shares[i].Save_in_Database();
                            if (log == "true")
                            {
                                Logger.Log("Share successfully processed for date: " + shares[i]._date + ". Result: " + save_in_db_tracker.ToString(), tk[0].Ticker, tk[0].Ticker);
                            }
                        }
                    }
                    else
                    {
                        if (log == "true")
                        {
                            Logger.Log("No Tickers to process.", tk[0].Ticker, tk[0].Ticker);
                        }
                    }
                } else
                {
                    if (log == "true")
                    {
                        Logger.Log("Not a good time to run the process. system_hour = " + current_time.Hour + " config_file_hour = " + hour + ". system_minute = " + current_time.Minute + " config_file_minute = " + minute);
                    }
                }
            } catch (Exception ex)
            {
                Logger.Log("Windows Service Failed. Ticker: | " + tk[0].Ticker + " | Result: | " + result + " | Error: [" + ex.Message + "]", tk[0].Ticker, tk[0].Ticker);
            } finally {}
        }
    }
}
