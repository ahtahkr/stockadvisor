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
                    //if (true)
                {
                    Library.Logger.Log("Getting data for " + dt.DayOfWeek.ToString() + " on " + dt.Hour + " UTC", "Stock_Changes_Send_Email");
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

                    Library.Logger.Log("Parameters supplied " + JsonConvert.SerializeObject(param), "Stock_Changes_Send_Email");

                    string companies = AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                        "[fsn].[Share_Change]"
                        , connection_string
                        , param);

                    Library.Logger.Log("Companies Received " + companies, "Stock_Changes_Send_Email");

                    List<Library.Change> changes = JsonConvert.DeserializeObject<List<Library.Change>>(companies);

                    Library.Logger.Log("List of companies " + JsonConvert.SerializeObject(changes), "Stock_Changes_Send_Email");

                    string body = Environment.NewLine + Environment.NewLine + "Ascending Changes" + Environment.NewLine;
                    
                        for (int a = 0; a < changes.Count; a++)
                        {
                            body += changes[a].Symbol + " - " + changes[a].Url + Environment.NewLine;
                        }

                    body += Environment.NewLine + Environment.NewLine + "Share ChangePercentage" + Environment.NewLine;


                    int Change_percentage = -5;
                    int Max_Close = 5;
                    try
                    {
                        Change_percentage = Convert.ToInt32(ConfigurationManager.AppSettings["Change_Percentage"]);
                        MaxHigh = Convert.ToInt32(ConfigurationManager.AppSettings["Max_Close"]);
                    }
                    catch
                    {
                        Change_percentage = -5;
                        Max_Close = 5;
                    }
                    if (Change_percentage == 0) { Change_percentage = -5; }
                    if (Max_Close == 0) { Max_Close = 5; }

                    param = new Dictionary<string, object>();
                    param.Add("changePercentage", Change_percentage);
                    param.Add("maxClose", Max_Close);

                    Library.Logger.Log("Parameters supplied " + JsonConvert.SerializeObject(param), "Stock_Changes_Send_Email");

                    companies = AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                        "[fsn].[Share_ChangePercentage]"
                        , connection_string
                        , param);

                    Library.Logger.Log("Shares Received " + companies, "Stock_Changes_Send_Email");

                    List<Library.Share> shares = JsonConvert.DeserializeObject<List<Library.Share>>(companies);

                    Library.Logger.Log("List of shares " + JsonConvert.SerializeObject(shares), "Stock_Changes_Send_Email");

                    for (int a = 0; a < shares.Count; a++)
                    {
                        body += "https://www.tradingview.com/chart/?symbol=" + shares[a].Symbol + " : " + JsonConvert.SerializeObject(shares[a]) + Environment.NewLine;
                    }

                    body += "";

                    Library.Logger.Log("Email Body: " + body, "Stock_Changes_Send_Email");

                    string smtp_server = Convert.ToString(ConfigurationManager.AppSettings["smtp_server"]);
                        int smtp_server_port = Convert.ToInt32(ConfigurationManager.AppSettings["smtp_server_port"]);
                        string smtp_username = Convert.ToString(ConfigurationManager.AppSettings["smtp_username"]);
                        string smtp_password = Convert.ToString(ConfigurationManager.AppSettings["smtp_password"]);
                        string[] recepient = { "ahtahkr@yahoo.com" };
                        try
                        {
                            Library.Utility.Email(smtp_server, smtp_server_port, smtp_username, smtp_password, "ahtahkr@gmail.com", recepient, "Changes", body);
                            Library.Logger.Log("Email Sent. " + smtp_server + " " + smtp_server_port + " " + smtp_username + " " + smtp_password + " " + JsonConvert.SerializeObject(recepient), "Stock_Changes_Send_Email");
                        } catch (Exception ex)
                        {
                            Library.Logger.Log_Error("Email Sent failed. Error Message:" + ex.Message, "Error_Stock_Changes_Send_Email");
                        }                    

                } else
                {
                    Library.Logger.Log("Not Getting data for " + dt.DayOfWeek.ToString() + " on " + dt.Hour + " UTC", "Stock_Changes_Send_Email");
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
