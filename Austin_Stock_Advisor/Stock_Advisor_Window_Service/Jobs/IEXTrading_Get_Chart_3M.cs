using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;

namespace AustinStockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private void IEXTrading_Get_Chart_Range(object sender = null, ElapsedEventArgs e = null)
        {
            string log = "IEXTrading_Get_Chart_Range started.";

            string run = ConfigurationManager.AppSettings["IEXTrading_Get_Chart_Range"];

            if (run.Equals("0"))
            {
                return;
            }

            string connection_string = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["environment"]].ConnectionString;
            string symbol = "beforetry";
            string result = "beforetry";
            string webapi = "beforetry";
            string range = "beforetry";

            List<AustinStockAdvisor.Library.Share> shares = new List<AustinStockAdvisor.Library.Share>();
            try
            {
                result = AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                "[fsn].[Company_Get_Symbol_ChartRange]", connection_string);

                log = "Got company from db: " + result;

                /* MethodFullName. */
                string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";

                if (String.IsNullOrEmpty(result) || result.Equals("[]")) {
                    log = "Not Getting data for result: [" + result + "]";
                    Library.Logger.Log("Not Getting data for result: [" + result + "]", "IEXTrading_Get_Chart_Range");
                }
                else
                {
                    
                    dynamic stuff1 = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                    symbol = stuff1[0].Symbol;

                    range = stuff1[0].Range;

                    log = "Getting data for symbol: " + symbol + " range: " + range;

                    webapi = AustinStockAdvisor.IEXTrading.WebApi.V_1.Chart(symbol, range);

                    log = "symbol: " + symbol + " range: " + range + " webapi: " + webapi;

                    shares = JsonConvert.DeserializeObject<List<AustinStockAdvisor.Library.Share>>(webapi);

                    log = "symbol: " + symbol + " range: " + range + " shares: " + JsonConvert.SerializeObject(shares);

                    if (shares.Count > 0)
                    {
                        for (int a = 0; a < shares.Count; a++)
                        {
                            shares[a].Symbol = symbol;
                            shares[a].Share_Insert_Update(connection_string);
                        }

                        log = "symbol: " + symbol + " range: " + range + " shares inserted: " + shares.Count;

                        AustinStockAdvisor.Library.Company company = new AustinStockAdvisor.Library.Company();
                        company.Symbol = shares[0].Symbol;
                        company.Company_Update_IEX_Chart_3M(connection_string);

                        log = "symbol: " + symbol + " range: " + range + " shares inserted and IEX_Chart_3m updated.";

                    } else
                    {
                        log = "symbol: " + symbol + " range: " + range + " shares.count: " + shares.Count;
                        
                        AustinStockAdvisor.Library.Company company = new AustinStockAdvisor.Library.Company();
                        company.Symbol = symbol;
                        company.Company_Alter_IEX_Trading(connection_string);

                        log = "symbol: " + symbol + " range: " + range + " IEX_Trading in Company table updated (not in catch).";
                    }
                }
            
            } catch (Exception ex)
            {

                /* MethodFullName. */
                string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";
                Library.Logger.Log_Error(methodfullname + " result ["+result+"] symbol:["+symbol+"] webapi: ["+webapi+"] Error Msg: " + ex.Message + ". Shares List: ["+ JsonConvert.SerializeObject(shares)+"]" );

                log = "symbol: " + symbol + " range: " + range + " (in catch).";

                AustinStockAdvisor.Library.Company company = new AustinStockAdvisor.Library.Company();
                company.Symbol = symbol;
                company.Company_Alter_IEX_Trading(connection_string);

                log = "symbol: " + symbol + " range: " + range + " IEX_Trading in Company table updated (in catch).";
            }
            Library.Logger.Log(log, "IEXTrading_Get_Chart_Range");
        }

        private void IEXTrading_Market_Previous(object sender = null, ElapsedEventArgs e = null)
        {
            try
            {
                ArrayList DAYS = new ArrayList(5);
                DAYS.Add("Monday");
                DAYS.Add("Tuesday");
                DAYS.Add("Wednesday");
                DAYS.Add("Thursday");
                DAYS.Add("Friday");
                DAYS.Add("Saturday");
                DateTime dt = DateTime.UtcNow;

                if (DAYS.Contains(dt.DayOfWeek.ToString()) && (dt.Hour == 9))
                {
                    Library.Logger.Log("Getting data for " + dt.DayOfWeek.ToString()+ " on " + dt.Hour + " UTC", "IEXTrading_Market_Previous");
                    string connection_string = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["environment"]].ConnectionString;
                    string webapi = AustinStockAdvisor.IEXTrading.WebApi.V_1.Previous_Market();
                    AustinStockAdvisor.Library.Share SHARE = new AustinStockAdvisor.Library.Share();

                    JObject o = JObject.Parse(webapi);
                    foreach (var c in o)
                    {
                        string _share = c.ToString();
                        string[] _shares = _share.Split('{')[1].Split('}');
                        SHARE = JsonConvert.DeserializeObject<AustinStockAdvisor.Library.Share>('{' + _shares[0] + '}');
                        SHARE.Share_Insert_Update(connection_string);
                    }
                    Library.Logger.Log("Got data for " + dt.DayOfWeek.ToString() + " on " + dt.Hour + " UTC", "IEXTrading_Market_Previous");
                }
                else
                {
                    Library.Logger.Log("Not Getting data for " + dt.DayOfWeek.ToString() + " on " + dt.Hour + " UTC", "IEXTrading_Market_Previous");
                }
            }
            catch (Exception ex)
            {

                /* MethodFullName. */
                string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";
                Library.Logger.Log_Error(methodfullname + " Error Msg: " + ex.Message);
            }
        
        }
    }
}
