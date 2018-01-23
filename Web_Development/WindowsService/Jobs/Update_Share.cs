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
        private void Update_Share(object sender = null, ElapsedEventArgs e = null)
        {
            List<Ticker_Class> tk = null;
            string result = "Before TRY.";
            //string _dates = "";
            try
            {
                eventLog_i_am_active.WriteEntry(DateTime.Now + " : " + "AustinsFirstProject.StockAdvisor.WindowsService.Update_Shares");

                string detail_log = ConfigurationManager.AppSettings["log_detail_update_share_success"]; ;
                
                string apikey = ConfigurationManager.AppSettings["intrino_api_key"];

                tk = Library.Intrinio.Utility.Database.Get_Tickers();
                
                if (detail_log == "true")
                {
                    Logger.Log("Starting Update Share.", tk[0].Ticker, tk[0].Ticker);
                    Logger.Log("Ticker Processing: " + JsonConvert.SerializeObject(tk), tk[0].Ticker, tk[0].Ticker);
                }
                result = TIME_SERIES_DAILY.GET(tk[0].Ticker, apikey);

                List<Share> shares = JsonConvert.DeserializeObject<List<Share>>(result);

                for (int i = 0; i < shares.Count; i++)
                {
                    shares[i].Save_in_Database();
                    //_dates += shares[i]._date;
                    if (detail_log == "true")
                    {
                        Logger.Log("Share successfully processed for date: " + shares[i]._date, tk[0].Ticker, tk[0].Ticker);
                    }
                }

                string success = ConfigurationManager.AppSettings["log_update_share_success"];
                if (success == "true")
                {
                    /*foreach (var item in tk)
                    {
                    }*/
                    Logger.Log("Update_Share successful. Ticker: " + JsonConvert.SerializeObject(tk), tk[0].Ticker, tk[0].Ticker);
                }

            } catch (Exception ex)
            {
                Logger.Log("Windows Service Failed. Ticker: | " + tk[0].Ticker + " | Result: | " + result + " | Error: [" + ex.Message + "]", tk[0].Ticker, tk[0].Ticker);
            } finally
            {
                

            }
        }
    }
}
