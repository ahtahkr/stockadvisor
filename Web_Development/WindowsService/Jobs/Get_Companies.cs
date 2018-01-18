using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AustinFirstProject.StockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private void Get_Companies(object sender = null, ElapsedEventArgs e = null)
        {
            eventLog_i_am_active.WriteEntry(DateTime.Now + " : " + "AustinFirstProject.StockAdvisor.WindowsService.Get_Companies");
        }
    }
}
