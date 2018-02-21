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
            string go_ahead = Convert.ToString(ConfigurationManager.AppSettings["IEXTrading_Get_News"]);

            if (go_ahead.Equals("true"))
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
                }
                catch (Exception ex)
                {
                    Logger.Log_Error("Windows_Service IEXTrading_Get_News failed. Error Msg: " + ex.Message);
                }
            }
        }
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

        private void IEXTrading_Chart(object sender = null, ElapsedEventArgs e = null)
        {
            string go_ahead = Convert.ToString(ConfigurationManager.AppSettings["IEXTrading_Chart"]);

            if (go_ahead.Equals("true"))
            {
                try
                {
                    Chart oHLC = new Chart();
                    if (oHLC.Set_Symbol_Range_from_DB())
                    {
                        if (oHLC.Call_Api())
                        {
                            if (oHLC.Save_In_File())
                            {
                                Dictionary<string, object> dictionary = new Dictionary<string, object>
                                {
                                    { "Symbol", oHLC.Symbol }
                                };

                                Library.Database.ExecuteProcedure_Get("[fsn].[Symbol_Change_Five_Years_Data]", dictionary);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log_Error("Windows Service Failed. IEXTrading_Chart. Error: [" + ex.Message + "]");
                }
                finally { }
            }
        }

        private void IEXTrading_Chart_Date(object sender = null, ElapsedEventArgs e = null)
        {
            string go_ahead = Convert.ToString(ConfigurationManager.AppSettings["IEXTrading_Chart_Date"]);

            if (go_ahead.Equals("true"))
            {
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
