using AustinsFirstProject.Library;
using AustinsFirstProject.StockAdvisor.IEXTrading;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AustinsFirstProject.StockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private void IEXTrading_Chart(object sender = null, ElapsedEventArgs e = null)
        {
            string go_ahead = Convert.ToString(ConfigurationManager.AppSettings["IEXTrading_Chart"]);

            if (go_ahead.Equals("true"))
            {
                try
                {
                    Chart oHLC = new Chart();
                    if (oHLC.IEXTrading_Get_Symbol_For_Chart())
                    {
                        if (oHLC.Call_Api())
                        {
                            if (oHLC.Save_In_File())
                            {
                                Dictionary<string, object> dictionary = new Dictionary<string, object>
                                {
                                    { "Symbol", oHLC.Symbol }
                                };

                                Library.Database.ExecuteProcedure_Get("[fsn].[Symbol_IEXTrading_Update_Chart]", dictionary);
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
    }
}
