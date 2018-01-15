using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;


namespace MyNewService
{
	public partial class Service1 : System.ServiceProcess.ServiceBase
	{
		private System.Diagnostics.EventLog eventLog1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private static int company_update_interval = 60; // seconds
        private Timer company_update_timer = new Timer(company_update_interval * 1000);

        public Service1()
		{
            InitializeComponent();

			// TODO: Add any initialization after the InitComponent call
			if(!System.Diagnostics.EventLog.SourceExists("DoDyLogSourse"))
			{	
				System.Diagnostics.EventLog.CreateEventSource("DoDyLogSourse","DoDyLog");
			}

				eventLog1.Source = "DoDyLogSourse";
				eventLog1.Log ="DoDyLog";
			
		}

		// The main entry point for the process
		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;
	
			// More than one user Service may run within the same process. To add
			// another service to this process, change the following line to
			// create a second service object. For example,
			//
			//   ServicesToRun = New System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
			//
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new MyNewService.Service1()};

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);	
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.eventLog1 = new System.Diagnostics.EventLog();
			((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
			// 
			// eventLog1
			// 
			this.eventLog1.Log = "DoDyLog";
			this.eventLog1.Source = "DoDyLogSource";
			// 
			// Service1
			// 
			this.ServiceName = "MyNewService";
			((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			// TODO: Add code here to start your service.
			eventLog1.WriteEntry("my service started");
            this.company_update_timer.Elapsed += Compress_DB_Backup;
            
            this.company_update_timer.Enabled = true;
        }

        /// <summary>
		/// Pause this service.
		/// </summary>
		protected override void OnPause()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            eventLog1.WriteEntry("my service paused");

            this.company_update_timer.Enabled = false;
        }

        /// <summary>
        /// Stop this service.
        /// </summary>
        protected override void OnStop()
		{
			// TODO: Add code here to perform any tear-down necessary to stop your service.
			eventLog1.WriteEntry("my service stoped");

            this.company_update_timer.Enabled = false;
        }

        /// <summary>
        /// Continue this service.
        /// </summary>
		protected override void OnContinue()
		{
			eventLog1.WriteEntry("my service is continuing in working");

            this.company_update_timer.Enabled = true;
        }
	}
}
