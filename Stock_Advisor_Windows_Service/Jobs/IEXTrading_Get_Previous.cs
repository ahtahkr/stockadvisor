using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;

namespace Stock_Advisor_Windows_Service.Jobs
{
    public partial class Service1 : ServiceBase
    {
        private void IEXTrading_Get_Symbol_ChartRange(object sender = null, ElapsedEventArgs e = null)
        {
            string connection_string = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["environment"]].ConnectionString;
            string input_directory = Convert.ToString(ConfigurationManager.AppSettings["Input_Directory"]);

            string result = Library.Database.ExecuteProcedure.Get("[fsn].[Company_Get_Symbol_ChartRange]", connection_string);

            try
            {
                dynamic stuff1 = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                string symbol = stuff1[0].Symbol;
                string range = stuff1[0].Range;

                IEXTrading.Operator.Save_Chart_Range_to_File(IEXTrading.Web_Api_Version.One_point_Zero, input_directory, symbol, range);
            } catch (Exception ex)
            {
                /* MethodFullName. */
                string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]";
                string message = "Connection String: " + connection_string;
                message += Environment.NewLine + "Input Directory: " + input_directory;
                message += Environment.NewLine + "Result: " + result;
                Library.Logger.Log_Error(methodfullname, message, ex.Message);
            }
        }
    }
}

/* MethodFullName. */
/* string methodfullname = "[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "]"; */
