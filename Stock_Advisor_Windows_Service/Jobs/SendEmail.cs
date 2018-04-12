using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Stock_Advisor_Windows_Service
{
    public partial class Service1 : ServiceBase
    {
        public void Send_Email(object sender = null, ElapsedEventArgs e = null)
        {
            try {
                DateTime dt = DateTime.UtcNow;

                ArrayList DAYS = new ArrayList(5)
                {
                    "Monday","Tuesday",
                    "Wednesday",
                    "Thursday",
                    "Friday",
                    "Saturday"
                };

                if (DAYS.Contains(dt.DayOfWeek.ToString()) && (dt.Hour == 10))
                {
                    string connection_string = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["environment"]].ConnectionString;

                    ProductionDatabase.Modal.Volume_Ascending_Collection volume_Ascending_Collection = new ProductionDatabase.Modal.Volume_Ascending_Collection();
                    volume_Ascending_Collection.Get_from_Database(connection_string);

                    string body = volume_Ascending_Collection.Get_Email_Body();

                    if (!String.IsNullOrEmpty(body))
                    {
                        string smtp_server = Convert.ToString(ConfigurationManager.AppSettings["smtp_server"]);
                        int smtp_server_port = Convert.ToInt32(ConfigurationManager.AppSettings["smtp_server_port"]);
                        string smtp_username = Convert.ToString(ConfigurationManager.AppSettings["smtp_username"]);
                        string smtp_password = Convert.ToString(ConfigurationManager.AppSettings["smtp_password"]);
                        string[] recepient = { "ahtahkr@yahoo.com" };
                        try
                        {
                            Library.Email.Send(smtp_server, smtp_server_port, smtp_username, smtp_password, "ahtahkr@gmail.com", recepient, "Changes", body);
                        }
                        catch (Exception ex)
                        {
                            /* MethodFullName. */
                            string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";

                            Library.Logger.Log_Error(methodfullname, "Failure Sending Email", ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                /* MethodFullName. */
                string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";
                Library.Logger.Log_Error(methodfullname, "Error", ex.Message);
            }
        }
    }
}
