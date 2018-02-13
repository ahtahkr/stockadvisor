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
using AustinsFirstProject.StockAdvisor.IEXTrading;

namespace AustinsFirstProject.StockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {

        private void Update_Share_IEXTrading_Chart(object sender = null, ElapsedEventArgs e = null)
        {
            List<Ticker_Class> tk = null;
            try
            {
                DateTime current_time = DateTime.Now;
                int current_hour_min = (current_time.Hour * 60) + current_time.Minute;

                int hour = Convert.ToInt32(ConfigurationManager.AppSettings["IEXTrading_Chart_hour"]);
                int minute = Convert.ToInt32(ConfigurationManager.AppSettings["IEXTrading_Chart_minute"]);

                string chart_option = Convert.ToString(ConfigurationManager.AppSettings["IEXTrading_Chart_option"]);

                int hour_min = (hour * 60) + minute;

                //if (hour_min == 0)
                //{
                //    hour_min = (18 * 60) + 00;
                //}

                if (current_hour_min >= hour_min)
                {
                    tk = StockAdvisor.IEXTrading.Utility.Database.Get_Tickers();
                    Chart oHLC = new Chart();
                    if (tk.Count > 0)
                    {
                        if (oHLC.Call_Api(tk[0].Ticker))
                        {
                            oHLC.Save_to_File();
                            DatabaseUpdateHelper.Update_Company_IEXTrading(tk[0].Ticker, 1);
                        }
                        else
                        {
                            DatabaseUpdateHelper.Update_Company_IEXTrading(tk[0].Ticker, 0);
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                Logger.Log_Error("Windows Service Failed. Update_Share_IEXTrading_Previous. Ticker: | " + tk[0].Ticker + " | Error: [" + ex.Message + "]", tk[0].Ticker, tk[0].Ticker);
            }
            finally { }
        }


        private void Update_Share_IEXTrading_Previous(object sender = null, ElapsedEventArgs e = null)
        {
            List<Ticker_Class> tk = null;
            try
            {
                DateTime current_time = DateTime.Now;
                int current_hour_min = (current_time.Hour * 60) + current_time.Minute;

                int hour = Convert.ToInt32(ConfigurationManager.AppSettings["IEXTrading_Previous_hour"]);
                int minute = Convert.ToInt32(ConfigurationManager.AppSettings["IEXTrading_Previous_minute"]);

                int hour_min = (hour * 60) + minute;

                if (hour_min == 0)
                {
                    hour_min = (18 * 60) + 00;
                }

                if (current_hour_min >= hour_min)
                {
                    tk = StockAdvisor.IEXTrading.Utility.Database.Get_Tickers();
                    Previous oHLC = new Previous();
                    if (tk.Count > 0)
                    {
                        if (oHLC.Call_Api(tk[0].Ticker))
                        {
                            oHLC.Save_to_File();
                            DatabaseUpdateHelper.Update_Company_IEXTrading(tk[0].Ticker, 1);
                        }
                        else
                        {
                            DatabaseUpdateHelper.Update_Company_IEXTrading(tk[0].Ticker, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log_Error("Windows Service Failed. Update_Share_IEXTrading_Previous. Ticker: | " + tk[0].Ticker + " | Error: [" + ex.Message + "]", tk[0].Ticker, tk[0].Ticker);
            }
            finally { }
        }


        private void Update_Share_IEXTrading_OHLC(object sender = null, ElapsedEventArgs e = null)
        {
            List<Ticker_Class> tk = null;
            try
            {
                DateTime current_time = DateTime.Now;
                int current_hour_min = (current_time.Hour * 60) + current_time.Minute;
                
                int hour = Convert.ToInt32(ConfigurationManager.AppSettings["IEXTrading_OHLC_hour"]);
                int minute = Convert.ToInt32(ConfigurationManager.AppSettings["IEXTrading_OHLC_minute"]);

                int hour_min = (hour * 60) + minute;

                if (hour_min == 0)
                {
                    hour_min = (18 * 60) + 00;
                }

                if (current_hour_min >= hour_min)
                {
                    tk = StockAdvisor.IEXTrading.Utility.Database.Get_Tickers();
                    OHLC oHLC = new OHLC();
                    if (tk.Count > 0)
                    {
                        if (oHLC.Call_Api(tk[0].Ticker))
                        {
                            oHLC.Save_to_File();
                            DatabaseUpdateHelper.Update_Company_IEXTrading(tk[0].Ticker, 1);
                        } else
                        {
                            DatabaseUpdateHelper.Update_Company_IEXTrading(tk[0].Ticker, 0);
                        }                        
                    }                    
                }
            } catch (Exception ex)
            {
                Logger.Log_Error("Windows Service Failed. Update_Share_IEXTrading_OHLC. Ticker: | " + tk[0].Ticker + " | Error: [" + ex.Message + "]", tk[0].Ticker, tk[0].Ticker);
            } finally {}
        }
    }
}
