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

namespace AustinStockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        public void Stock_Changes_Send_Email(object sender = null, ElapsedEventArgs e = null)
        {
            try
            {
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
                    Library.Logger.Log("Getting data for " + dt.DayOfWeek.ToString(), "Stock_Changes_Send_Email");
                    string connection_string = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["environment"]].ConnectionString;

                    int WeeksToGoBack = Convert.ToInt32(ConfigurationManager.AppSettings["WeeksToGoBack"]);
                    int MaxHigh = Convert.ToInt32(ConfigurationManager.AppSettings["MaxHigh"]);
                    int MinChange = Convert.ToInt32(ConfigurationManager.AppSettings["MinChange"]);
                    int Avg_Volume = Convert.ToInt32(ConfigurationManager.AppSettings["Avg_Volume"]);

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("WeeksToGoBack", WeeksToGoBack);
                    param.Add("Max_High", MaxHigh);
                    param.Add("Minimum_Change", MinChange);
                    param.Add("Avg_Volume", Avg_Volume);

                    string companies = AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                        "[fsn].[Share_Change]"
                        , connection_string
                        , param);

                    List<Library.Change> changes = JsonConvert.DeserializeObject<List<Library.Change>>(companies);
                    string body = Environment.NewLine + Environment.NewLine + "Ascending Changes" + Environment.NewLine;
                    if (changes.Count > 0)
                    {
                        for (int a = 0; a < changes.Count; a++)
                        {
                            body += changes[a].Symbol + " - " + changes[a].Url + Environment.NewLine;
                        }
                        body += "";

                        string smtp_server = Convert.ToString(ConfigurationManager.AppSettings["smtp_server"]);
                        int smtp_server_port = Convert.ToInt32(ConfigurationManager.AppSettings["smtp_server_port"]);
                        string smtp_username = Convert.ToString(ConfigurationManager.AppSettings["smtp_username"]);
                        string smtp_password = Convert.ToString(ConfigurationManager.AppSettings["smtp_password"]);
                        string[] recepient = { "ahtahkr@yahoo.com" };
                        Library.Utility.Email(smtp_server, smtp_server_port, smtp_username, smtp_password, "ahtahkr@gmail.com", recepient, "Changes", body);
                        Library.Logger.Log("Email Sent. " + smtp_server + " " + smtp_server_port +" " + smtp_username +" " + smtp_password + " " + JsonConvert.SerializeObject(recepient), "Stock_Changes_Send_Email");
                    }
                } else
                {
                    Library.Logger.Log("Not Getting data for " + dt.DayOfWeek.ToString(), "Stock_Changes_Send_Email");
                }
            } catch (Exception ex)
            {
                /* MethodFullName. */
                string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";
                Library.Logger.Log_Error(methodfullname + " . Error Msg: " + ex.Message);
            }
        }
    }
}
