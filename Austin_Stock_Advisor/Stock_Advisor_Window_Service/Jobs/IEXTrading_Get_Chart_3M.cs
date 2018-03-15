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
            string connection_string = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["environment"]].ConnectionString;

            AustinStockAdvisor.Library.Share sh = new AustinStockAdvisor.Library.Share();
            sh.Symbol = "Before Try";

            List<AustinStockAdvisor.Library.Share> shares = new List<AustinStockAdvisor.Library.Share>();
            shares.Add(sh);
            string webapi = "Before Try";

            try
            {
                string result = AustinStockAdvisor.Library.Database.ExecuteProcedure.Get(
                "[fsn].[Company_Get_Symbol_ChartRange]", connection_string);

                dynamic stuff1 = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                string symbol = stuff1[0].Symbol;
                string range = stuff1[0].Range;

                webapi = AustinStockAdvisor.IEXTrading.WebApi.V_1.Chart(symbol, range);
                shares = JsonConvert.DeserializeObject<List<AustinStockAdvisor.Library.Share>>(webapi);
                for (int a = 0; a < shares.Count; a++)
                {
                    shares[a].Symbol = symbol;
                    shares[a].Share_Insert_Update(connection_string);
                }

                AustinStockAdvisor.Library.Company company = new AustinStockAdvisor.Library.Company();
                company.Symbol = shares[0].Symbol;
                    company.Company_Update_IEX_Chart_3M(connection_string);
            
            } catch (Exception ex)
            {

                /* MethodFullName. */
                string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";
                Library.Logger.Log_Error(methodfullname + " . Error Msg: " + ex.Message + ". Web Api returned value: [" + webapi + "] Shares List: ["+ JsonConvert.SerializeObject(shares)+"]" );

                AustinStockAdvisor.Library.Company company = new AustinStockAdvisor.Library.Company();
                company.Symbol = shares[0].Symbol;
                company.Company_Update_IEX_Chart_3M(connection_string);

                return;
            }
        }

        private void IEXTrading_Market_Previous(object sender = null, ElapsedEventArgs e = null)
        {
            ArrayList DAYS = new ArrayList(5);
            DAYS.Add("Tuesday");
            DAYS.Add("Wednesday");
            DAYS.Add("Thursday");
            DAYS.Add("Friday");
            DAYS.Add("Saturday");
            string day = DateTime.UtcNow.DayOfWeek.ToString();

            if (DAYS.Contains(day))
            {
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
            }
        
        }
    }
}
