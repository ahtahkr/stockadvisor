using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AustinStockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private static int three_m_interval = 10; // seconds
        private Timer three_m_timer = new Timer(three_m_interval * 1000);

        public Service1()
        {
            InitializeComponent();
            CreateLog();
        }

        protected override void OnStart(string[] args)
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service started.");

            this.three_m_timer.Elapsed += Stock_3M_Download;
            this.three_m_timer.Enabled = true;
        }

        protected override void OnStop()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service stopped.");
            this.three_m_timer.Enabled = false;
        }
        protected override void OnPause()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service paused.");

            this.three_m_timer.Enabled = false;
        }
        protected override void OnContinue()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service continued.");

            this.three_m_timer.Enabled = true;
        }

        private void CreateLog()
        {
            if (!System.Diagnostics.EventLog.SourceExists("AustinWindowsService"))
            {
                System.Diagnostics.EventLog.CreateEventSource("AustinWindowsService", "AustinWindowsLog");
            }

            //if (!System.Diagnostics.EventLog.SourceExists("IAMACTIVE")) { System.Diagnostics.EventLog.CreateEventSource("IAMACTIVE", "IAMACTIVE_Log"); }

            eventLog_i_am_active.Source = "AustinWindowsService";
            eventLog_i_am_active.Log = "AustinWindowsLog";

            //eventLog_update_share.Source = "AustinWindowsService";
            //eventLog_update_share.Log = "AustinWindowsLog";
        }
    }
}
