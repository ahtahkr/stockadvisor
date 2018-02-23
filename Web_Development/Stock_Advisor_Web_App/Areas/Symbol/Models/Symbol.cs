using AustinsFirstProject.Library;
using AustinsFirstProject.StockAdvisor.IEXTrading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AustinsFirstProject.WebApplication.Areas.Symbol.Models
{
    public class Symbol
    {
        public string Connection_String { get; set; }

        public List<Symbol_> Symbols { get; set; }

        public Symbol()
        {
            this.Connection_String = "";
            this.Symbols = new List<Symbol_>();
        }

        public bool Get_Symbols()
        {
            if (String.IsNullOrEmpty(this.Connection_String))
            {
                /* MethodFullName. */
                Logger.Log_Error("[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "] Get_Symbols was called when connection string was null or empty.");
                return false;
            }
            string result = "Before TRY";
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();

                result = Library.Database.ExecuteProcedure_Get(
                    "[webApp].[Get_Symbol]"
                    , param, this.Connection_String);
                this.Symbols = JsonConvert.DeserializeObject<List<Symbol_>>(result);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log_Error("[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "] FAILED. Result: " + result + ". Message: " + ex.Message);
            }
            return false;
        }
    }
}
