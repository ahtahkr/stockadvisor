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

namespace AustinsFirstProject.StockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private static int i_am_active_interval = 1; // seconds
        private Timer i_am_active_timer = new Timer(i_am_active_interval * 1000);

        // once in six months
        private static int IEXTrading_Download_Companies_interval = 6 * 30 * 24 * 60 * 60;
        private Timer IEXTrading_Download_Companies_timer = new Timer(IEXTrading_Download_Companies_interval * 1000);

        private static int update_share_interval = 10; // seconds
        private Timer update_share_timer = new Timer(update_share_interval * 1000);

        public Service1()
        {
            InitializeComponent();
            CreateLog();
        }

        protected override void OnStart(string[] args)
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service started.");

            this.IEXTrading_Download_Companies_timer.Elapsed += IEXTrading_Download_Symbols;
            this.IEXTrading_Download_Companies_timer.Enabled = true;

            this.update_share_timer.Elapsed += Update_Share_IEXTrading_Chart;
            this.update_share_timer.Enabled = true;

            this.i_am_active_timer.Elapsed += I_AM_ACTIVE;
            this.i_am_active_timer.Enabled = true;
        }

        protected override void OnStop()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service stopped.");

            this.update_share_timer.Enabled = false;
            this.i_am_active_timer.Enabled = false;
            this.IEXTrading_Download_Companies_timer.Enabled = false;
        }
        protected override void OnPause()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service paused.");

            this.update_share_timer.Enabled = false;
            this.i_am_active_timer.Enabled = false;
            this.IEXTrading_Download_Companies_timer.Enabled = false;
        }
        protected override void OnContinue()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service continued.");

            this.update_share_timer.Enabled = true;
            this.i_am_active_timer.Enabled = true;
            this.IEXTrading_Download_Companies_timer.Enabled = true;
        }

        private void CreateLog()
        {
            if (!System.Diagnostics.EventLog.SourceExists("AustinWindowsService"))
            {
                System.Diagnostics.EventLog.CreateEventSource("AustinWindowsService", "AustinWindowsLog");
            }

            if (!System.Diagnostics.EventLog.SourceExists("IAMACTIVE")) { System.Diagnostics.EventLog.CreateEventSource("IAMACTIVE", "IAMACTIVE_Log"); }

            eventLog_i_am_active.Source = "AustinWindowsService";
            eventLog_i_am_active.Log = "AustinWindowsLog";

            //eventLog_update_share.Source = "AustinWindowsService";
            //eventLog_update_share.Log = "AustinWindowsLog";
        }
    }
}
