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
            try
            {
                eventLog_i_am_active.WriteEntry(DateTime.Now + " : " + "AustinsFirstProject.StockAdvisor.WindowsService.Update_Shares");

                string apikey = ConfigurationManager.AppSettings["intrino_api_key"];

                tk = Library.Intrinio.Utility.Database.Get_Tickers();
                result = TIME_SERIES_DAILY.GET(tk[0].Ticker, apikey);

                List<Share> shares = JsonConvert.DeserializeObject<List<Share>>(result);

                for (int i = 0; i < shares.Count; i++)
                {
                    Console.WriteLine(shares[i].Save_in_Database());
                }
            } catch (Exception ex)
            {
                Logger.Log_Error("Windows Service Failed. Ticker: | " + tk[0].Ticker + " | Result: | " + result + " | Error: [" + ex.Message + "]", "Update_Shares_");
            } finally
            {
                string success = ConfigurationManager.AppSettings["log_update_share_success"];
                if (success == "true")
                {
                    foreach (var item in tk)
                    {
                        Console.WriteLine(item);
                    }
                    Logger.Log("Update_Share successful. Ticker: [" + JsonConvert.SerializeObject(tk) + "]");
                }

            }
        }
    }
}
