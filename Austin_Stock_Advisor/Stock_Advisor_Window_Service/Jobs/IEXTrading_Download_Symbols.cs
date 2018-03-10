using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;

namespace AustinStockAdvisor.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private void IEXTrading_Get_Symbols(object sender = null, ElapsedEventArgs e = null)
        {
            List<LibraryCore.Company> Companies = new List<LibraryCore.Company>();
            try
            {
                Companies
                    = JsonConvert.DeserializeObject<List<LibraryCore.Company>>(
                        IEXTrading.WebApi.V_1.Symbols());
            } catch (Exception ex)
            {

                /* MethodFullName. */
                string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";
                LibraryStandard.Logger.Log_Error(methodfullname + " Failed creating company list. Error Msg: " + ex.Message);
                return;
            }

            string connection_string = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["environment"]].ConnectionString;
            for (int a = 0; a < Companies.Count; a++)
            {
                LibraryStandard.Logger.Log(Companies[a].Company_Insert_Update(connection_string));
            }
        }
    }
}
