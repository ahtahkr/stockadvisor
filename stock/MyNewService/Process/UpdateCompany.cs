using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace MyNewService
{
    public partial class Service1 : System.ServiceProcess.ServiceBase
    {
        private void UpdateCompany(object sender = null, ElapsedEventArgs e = null)
        {
            eventLog1.WriteEntry(DateTime.Now + " UpdateCompany is active.");

        }
    }
}
