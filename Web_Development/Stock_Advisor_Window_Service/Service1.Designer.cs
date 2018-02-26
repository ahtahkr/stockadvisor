namespace StockAdvisor.WindowsService
{
    partial class Service1
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.eventLog_i_am_active = new System.Diagnostics.EventLog();
            //this.eventLog_update_share = new System.Diagnostics.EventLog();

            ((System.ComponentModel.ISupportInitialize)(this.eventLog_i_am_active)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.eventLog_update_share)).BeginInit();

            /* components = new System.ComponentModel.Container(); */

            this.ServiceName = "Austin_Stock_WindowsService";
            ((System.ComponentModel.ISupportInitialize)(this.eventLog_i_am_active)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.eventLog_update_share)).EndInit();
        }

        #endregion


        //private System.Diagnostics.EventLog eventLog_update_share;
        private System.Diagnostics.EventLog eventLog_i_am_active;
    }
}
