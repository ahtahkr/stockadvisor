using Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace StockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private void I_AM_ACTIVE(object sender = null, ElapsedEventArgs e = null)
        {
            string base_directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "I_AM_ACTIVE.log");

            if (!Directory.Exists(base_directory))
            {
                Directory.CreateDirectory(base_directory);
            }

            File.WriteAllText(filename, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + " : " + "I AM ACTIVE.");
        }
    }
}
