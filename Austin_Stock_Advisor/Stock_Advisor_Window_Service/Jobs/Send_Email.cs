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
            DateTime dt = DateTime.UtcNow;

            ArrayList DAYS = new ArrayList(5)
            {
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday"
            };

            if (DAYS.Contains(dt.DayOfWeek.ToString()) && (dt.Hour == 10))
            {
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
                string body = "<div>";
                if (changes.Count > 0)
                {
                    for (int a = 0; a < changes.Count; a++)
                    {
                        body += "<p><a href=\"" + changes[a].Url + "\" target = \"_blank\">" + changes[a].Symbol + "</a> - " + changes[a].Avg_Volume + " - " + changes[a].Max_High + " - " + changes[a].WeeksToGoBack + "</p>";
                    }
                    body += "</div>";
                    string smtp_server = Convert.ToString(ConfigurationManager.AppSettings["smtp_server"]);
                    int smtp_server_port = Convert.ToInt32(ConfigurationManager.AppSettings["smtp_server_port"]);
                    string smtp_username = Convert.ToString(ConfigurationManager.AppSettings["smtp_username"]);
                    string smtp_password = Convert.ToString(ConfigurationManager.AppSettings["smtp_password"]);
                    string[] recepient = { "ahtahkr@yahoo.com" };
                    Library.Utility.Email(smtp_server, smtp_server_port, smtp_username, smtp_password, "ahtahkr@gmail.com", recepient, "Changes", body);
                }
            }
        }
    }
}
