﻿using System;
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

        private static int IEXTrading_Chart_Date_interval = 3; // seconds
        private Timer IEXTrading_Chart_Date_timer = new Timer(IEXTrading_Chart_Date_interval * 1000);
        
        private static int Upload_Files_interval = 3;
        private Timer Upload_Files_timer = new Timer(Upload_Files_interval * 1000);

        private static int IEXTrading_Chart_interval = 3;
        private Timer IEXTrading_Chart_timer = new Timer(IEXTrading_Chart_interval * 1000);

        private static int IEXTrading_Tops_Last_interval = 60 * 60;
        private Timer IEXTrading_Tops_Last_timer = new Timer(IEXTrading_Tops_Last_interval * 1000);

        private static int IEXTrading_Get_News_interval = 60 * 60;
        private Timer IEXTrading_Get_News_timer = new Timer(IEXTrading_Get_News_interval * 1000);

        public Service1()
        {
            InitializeComponent();
            CreateLog();
        }

        protected override void OnStart(string[] args)
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service started.");

            this.Upload_Files_timer.Elapsed += Upload_Files;
            this.Upload_Files_timer.Enabled = true;

            this.IEXTrading_Chart_timer.Elapsed += IEXTrading_Chart;
            this.IEXTrading_Chart_timer.Enabled = true;

            this.IEXTrading_Chart_Date_timer.Elapsed += IEXTrading_Chart_5y;
            this.IEXTrading_Chart_Date_timer.Enabled = true;

            this.IEXTrading_Tops_Last_timer.Elapsed += IEXTrading_Top_Last;
            this.IEXTrading_Tops_Last_timer.Enabled = true;

            this.IEXTrading_Get_News_timer.Elapsed += IEXTrading_Get_News;
            this.IEXTrading_Get_News_timer.Enabled = true;

            this.i_am_active_timer.Elapsed += I_AM_ACTIVE;
            this.i_am_active_timer.Enabled = true;
        }

        protected override void OnStop()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service stopped.");
            this.i_am_active_timer.Enabled = false;
            this.IEXTrading_Chart_timer.Enabled = false;
            this.IEXTrading_Chart_Date_timer.Enabled = false;
            this.IEXTrading_Tops_Last_timer.Enabled = false;
            this.IEXTrading_Get_News_timer.Enabled = false;

            this.Upload_Files_timer.Enabled = false;
        }
        protected override void OnPause()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service paused.");

            this.i_am_active_timer.Enabled = false;
            this.IEXTrading_Chart_timer.Enabled = false;
            this.IEXTrading_Chart_Date_timer.Enabled = false;
            this.IEXTrading_Tops_Last_timer.Enabled = false;
            this.IEXTrading_Get_News_timer.Enabled = false;

            this.Upload_Files_timer.Enabled = false;
        }
        protected override void OnContinue()
        {
            eventLog_i_am_active.WriteEntry("Austin Stock Windows Service continued.");

            this.i_am_active_timer.Enabled = true;
            this.IEXTrading_Chart_timer.Enabled = true;
            this.IEXTrading_Chart_Date_timer.Enabled = true;
            this.IEXTrading_Tops_Last_timer.Enabled = true;
            this.IEXTrading_Get_News_timer.Enabled = true;

            this.Upload_Files_timer.Enabled = true;
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
