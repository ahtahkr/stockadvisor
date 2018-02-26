using Library;
using System;
using System.ServiceProcess;
using System.Timers;
using System.Configuration;
using StockAdvisor.IEXTrading;

namespace StockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private void IEXTrading_Top_Last(object sender = null, ElapsedEventArgs e = null)
        {
            string go_ahead = Convert.ToString(ConfigurationManager.AppSettings["IEXTrading_Top_Last"]);

            if (go_ahead.Equals("true"))
            {
                try
                {
                    DateTime current_time = DateTime.UtcNow;
                    int current_hour = current_time.Hour;

                    int hour = Convert.ToInt32(ConfigurationManager.AppSettings["IEXTrading_Top_Last_hour"]);

                    if (current_hour == hour)
                    {
                        Lasts lasts = new Lasts();
                        if (lasts.Call_Api())
                        {
                            lasts.Save_In_File();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log_Error("Windows Service Failed. IEXTrading_Top_Last. Error: [" + ex.Message + "]");
                }
                finally { }
            }
        }
    }
}
