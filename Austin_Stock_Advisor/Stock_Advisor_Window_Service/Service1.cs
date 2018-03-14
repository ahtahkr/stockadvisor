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
        private static int iextrading_get_chart_Range_interval = 60; // seconds
        private Timer iextrading_get_chart_Range_timer = new Timer(iextrading_get_chart_Range_interval * 1000);

        private static int iextrading_previous_market_interval = 60 * 60; // seconds
        private Timer iextrading_previous_market_timer = new Timer(iextrading_previous_market_interval * 1000);

        private static int smtp = 60 * 60; // seconds
        private Timer smtp_timer = new Timer(smtp * 1000);

        public Service1()
        {
            InitializeComponent();
            CreateLog();
        }

        protected override void OnStart(string[] args)
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service started.");

            this.iextrading_get_chart_Range_timer.Elapsed += IEXTrading_Get_Chart_Range;
            this.iextrading_get_chart_Range_timer.Enabled = true;

            this.iextrading_previous_market_timer.Elapsed += IEXTrading_Market_Previous;
            this.iextrading_previous_market_timer.Enabled = true;

            this.smtp_timer.Elapsed += Stock_Changes_Send_Email;
            this.smtp_timer.Enabled = true;
        }

        protected override void OnStop()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service stopped.");
            this.iextrading_get_chart_Range_timer.Enabled = false;
            this.iextrading_previous_market_timer.Enabled = false;
            this.smtp_timer.Enabled = false;
        }
        protected override void OnPause()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service paused.");

            this.iextrading_get_chart_Range_timer.Enabled = false;
            this.iextrading_previous_market_timer.Enabled = false;
            this.smtp_timer.Enabled = false;
        }
        protected override void OnContinue()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service continued.");

            this.iextrading_get_chart_Range_timer.Enabled = true;
            this.iextrading_previous_market_timer.Enabled = true;
            this.smtp_timer.Enabled = true;
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
