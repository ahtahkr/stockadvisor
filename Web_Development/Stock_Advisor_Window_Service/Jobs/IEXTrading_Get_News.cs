using Library;
using Library.Stock;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Timers;
using System.Configuration;
using StockAdvisor.IEXTrading;

namespace StockAdvisor.WindowsService
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
                    /* MethodFullName. */
                    Logger.Log_Error("[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "] Error Msg: " + ex.Message);
                }
            }
        }
    }
}
