using System.ServiceProcess;
using System.Timers;

namespace Stock_Advisor_Windows_Service
{
    partial class Service1 : ServiceBase
    {
        private static int iextrading_get_chart_Range_interval = 10; // seconds
        private Timer iextrading_get_chart_Range_timer = new Timer(iextrading_get_chart_Range_interval * 1000);

        private static int iextrading_get_previous_interval = 60 * 60; // seconds
        private Timer iextrading_get_previous_timer = new Timer(iextrading_get_previous_interval * 1000);

        private static int process_file_interval = 5; // seconds
        private Timer process_file_timer = new Timer(process_file_interval * 1000);

        private static int send_email_interval = 60*60; // seconds
        private Timer send_email_timer = new Timer(send_email_interval * 1000);

        public Service1()
        {
            InitializeComponent();
            CreateLog();
        }

        protected override void OnStart(string[] args)
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service started.");

            this.iextrading_get_chart_Range_timer.Elapsed += IEXTrading_Get_Symbol_ChartRange;
            this.iextrading_get_chart_Range_timer.Enabled = true;

            this.iextrading_get_previous_timer.Elapsed += IEXTrading_Get_Previous;
            this.iextrading_get_previous_timer.Enabled = true;

            this.process_file_timer.Elapsed += Process_File;
            this.process_file_timer.Enabled = true;

            this.send_email_timer.Elapsed += Send_Email;
            this.send_email_timer.Enabled = true;
        }

        protected override void OnStop()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service stopped.");
            this.iextrading_get_chart_Range_timer.Enabled = false;
            this.iextrading_get_previous_timer.Enabled = false;
            this.process_file_timer.Enabled = false;
            this.send_email_timer.Enabled = false;
        }
        protected override void OnPause()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service paused.");
            this.iextrading_get_chart_Range_timer.Enabled = false;
            this.iextrading_get_previous_timer.Enabled = false;
            this.process_file_timer.Enabled = false;
            this.send_email_timer.Enabled = false;
        }
        protected override void OnContinue()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service continued.");
            this.iextrading_get_chart_Range_timer.Enabled = true;
            this.iextrading_get_previous_timer.Enabled = true;
            this.process_file_timer.Enabled = true;
            this.send_email_timer.Enabled = true;
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
