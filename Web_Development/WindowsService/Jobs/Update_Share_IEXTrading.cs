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
using System.IO;
using Newtonsoft.Json.Linq;

namespace AustinsFirstProject.StockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private void IEXTrading_Get_News(object sender = null, ElapsedEventArgs e = null)
        {
            try
            {
                News news = new News();
                if (news.Get_Symbols_From_Database())
                {
                    if (news.Call_Api())
                    {
                        if (news.Save_In_File())
                        {
                            /* Console.WriteLine("Saved in File."); */
                        }
                        else
                        {
                            /*Console.WriteLine("Fail : Save in file.");*/
                        }
                    }
                    else
                    {
                        /*Console.WriteLine("Fail : Call Api");*/
                    }
                }
                else
                {
                    /*Console.WriteLine("Fail : Get_Symbols_From_Database");*/
                }
            } catch (Exception ex)
            {
                Logger.Log_Error("Windows_Service IEXTrading_Get_News failed. Error Msg: " + ex.Message);
            }
        }
            private void IEXTrading_Top_Last(object sender = null, ElapsedEventArgs e = null)
        {
            string base_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "IEXTrading_Top_Last.log");

            if (!Directory.Exists(base_directory))
            {
                Directory.CreateDirectory(base_directory);
            }

            File.WriteAllText(filename, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + " : " + "IEXTrading_Top_Last.");
                                    
            try
            {
                DateTime current_time = DateTime.Now;
                int current_hour_min = (current_time.Hour * 60) + current_time.Minute;

                int hour = Convert.ToInt32(ConfigurationManager.AppSettings["IEXTrading_Top_Last_hour"]);
                int minute = Convert.ToInt32(ConfigurationManager.AppSettings["IEXTrading_Top_Last_minute"]);

                int hour_min = (hour * 60) + minute;

                if (hour_min == 0)
                {
                    hour_min = (18 * 60) + 00;
                }

                if (current_hour_min >= hour_min)
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

        private void IEXTrading_Chart(object sender = null, ElapsedEventArgs e = null)
        {
            string base_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "IEXTrading_Chart.log");

            if (!Directory.Exists(base_directory))
            {
                Directory.CreateDirectory(base_directory);
            }

            File.WriteAllText(filename, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + " : " + "IEXTrading_Chart.");

            try
            {
                Chart oHLC = new Chart();
                if (oHLC.Set_Symbol_Range_from_DB())
                {
                    oHLC.Call_Api();
                    oHLC.Save_In_File();
                }
            }
            catch (Exception ex)
            {
                Logger.Log_Error("Windows Service Failed. IEXTrading_Chart. Error: [" + ex.Message + "]");
            }
            finally { }
        }

        private void IEXTrading_Chart_Date(object sender = null, ElapsedEventArgs e = null)
        {
            string base_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "IEXTrading_Chart_Date.log");

            if (!Directory.Exists(base_directory))
            {
                Directory.CreateDirectory(base_directory);
            }

            File.WriteAllText(filename, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + " : " + "IEXTrading_Chart_log.");
            string result = "Before";
            try
            {
                Chart oHLC = new Chart();
                result = oHLC.Download_Chart_Date();
                if (!String.IsNullOrEmpty(result))
                {
                    dynamic jsonparse = JObject.Parse(result);

                    if (oHLC.Call_Api_Date(
                            jsonparse["Symbol"].ToString()
                            , jsonparse["Date"].ToString()
                            , true
                    ))
                    {
                        oHLC.Save_In_File();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log_Error("Windows Service Failed. IEXTrading_Chart_Date. Result: " + result + "Error: [" + ex.Message + "]");
            }
            finally { }
        }


        private void IEXTrading_Previous(object sender = null, ElapsedEventArgs e = null)
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
                            oHLC.Save_In_File();
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
                            oHLC.Save_In_File();
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
