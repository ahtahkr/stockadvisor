using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace StockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private void IEXTrading_Download_Symbols(object sender = null, ElapsedEventArgs e = null)
        {
            try
            {
                if (StockAdvisor.IEXTrading.Symbols.Download_Symbols())
                {

                } else
                {
                    Logger.Log_Error("Windows Service Failed. IEXTrading_Download_Symbols. Download_Symbols returned FALSE.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log_Error("Windows Service Failed. IEXTrading_Download_Symbols. Error: [" + ex.Message + "]");
            }
            finally { }
        }
    }
}