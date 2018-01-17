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

namespace AustinFirstProject.StockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private static int i_am_active_interval = 1; // seconds
        private Timer i_am_active_timer = new Timer(i_am_active_interval * 1000);

        public Service1()
        {
            InitializeComponent();
            CreateLog();
        }

        protected override void OnStart(string[] args)
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service started.");

            this.i_am_active_timer.Elapsed += I_AM_ACTIVE;

            this.i_am_active_timer.Enabled = true;
        }

        protected override void OnStop()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service stopped.");

            this.i_am_active_timer.Enabled = false;
        }
        protected override void OnPause()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service paused.");

            this.i_am_active_timer.Enabled = false;
        }
        protected override void OnContinue()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service continued.");

            this.i_am_active_timer.Enabled = true;
        }

        private void CreateLog()
        {
            if (!System.Diagnostics.EventLog.SourceExists("AustinWindowsService"))
            {
                System.Diagnostics.EventLog.CreateEventSource("AustinWindowsService", "AustinLog");
            }
            eventLog_i_am_active.Source = "AustinWindowsService";
            eventLog_i_am_active.Log = "AustinWindowsLog";
        }
    }
}
