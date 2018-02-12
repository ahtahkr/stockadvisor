using AustinsFirstProject.AlphaVantage;
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
        private void Update_Share_AlphaVantage(object sender = null, ElapsedEventArgs e = null)
        {
            List<Ticker_Class> tk = null;
            string result = "Before TRY.";
            string log_file_name = "Share_Updated_AlphaVantage";

            try
            {
                DateTime current_time = DateTime.Now;
                int current_hour_min = (current_time.Hour * 60) + current_time.Minute;
                
                int hour = Convert.ToInt32(ConfigurationManager.AppSettings["update_share_hour"]);
                int minute = Convert.ToInt32(ConfigurationManager.AppSettings["update_share_minute"]);
                string log = ConfigurationManager.AppSettings["log_update_share"];
                string apikey = ConfigurationManager.AppSettings["intrino_api_key"];
                string date_to_process = ConfigurationManager.AppSettings["update_share_date_to_process"];

                int hour_min = (hour * 60) + minute;

                if (current_hour_min >= hour_min)
                {
                    tk = Library.Intrinio.Utility.Database.Get_Tickers();

                    if (tk.Count > 0)
                    {
                        List<string> date_list = new List<string>();

                        if (date_to_process == "today")
                        {
                            date_list.Add(DateTime.Now.ToString("yyyy-MM-dd"));
                        }
                        result = TIME_SERIES_DAILY.GET(tk[0].Ticker, apikey, date_list);

                        List<Share> shares = JsonConvert.DeserializeObject<List<Share>>(result);
                        
                        object save_in_db_tracker;
                        for (int i = 0; i < shares.Count; i++)
                        {
                            save_in_db_tracker = shares[i].Save_in_Database();
                        }

                        if (shares.Count > 0)
                        {
                            DatabaseUpdateHelper.Update_WebApi_AlphaVantage(tk[0].Ticker, 1);
                        } else
                        {
                            DatabaseUpdateHelper.Update_WebApi_AlphaVantage(tk[0].Ticker, 0);
                        }
                        Logger.Log(tk[0].Ticker + "," + shares.Count, log_file_name);                        
                    }                    
                }
            } catch (Exception ex)
            {
                //eventLog_update_share.WriteEntry("Windows Service Failed. Ticker: | " + tk[0].Ticker + " | Result: | " + result + " | Error: [" + ex.Message + "]");
                Logger.Log_Error("Windows Service Failed. Ticker: | " + tk[0].Ticker + " | Result: | " + result + " | Error: [" + ex.Message + "]", tk[0].Ticker, tk[0].Ticker);
            } finally {}
        }
    }
}
