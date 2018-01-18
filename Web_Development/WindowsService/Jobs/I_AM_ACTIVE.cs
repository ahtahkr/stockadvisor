using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AustinsFirstProject.StockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private void I_AM_ACTIVE(object sender = null, ElapsedEventArgs e = null)
        {
            eventLog_i_am_active.WriteEntry(DateTime.Now + " : " + "AustinsFirstProject.StockAdvisor.WindowsService.I_AM_ACTIVE");
        }
    }
}
