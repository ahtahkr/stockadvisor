using AustinsFirstProject.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustinsFirstProject.StockAdvisor.IEXTrading
{
    public static class Symbols
    {
        public static bool Download_Symbols()
        {
            try
            {
                List<Symbol_> SYMBOLS = JsonConvert.DeserializeObject<List<Symbol_>>(
                                                Utility.HttpRequestor.Symbols());
                string full_file_name = "";

                do
                {
                    full_file_name = Utility.Get_Full_FileName_to_Save_Api_Result(full_file_name, "Symbol");
                } while (File.Exists(full_file_name));

                File.AppendAllText(full_file_name, JsonConvert.SerializeObject(SYMBOLS));
                return true;
            } catch (Exception ex)
            {
                Logger.Log_Error("AustinsFirstProject.StockAdvisor.IEXTrading.Symbols.Download_Symbols() failed. Error Msg: " + ex.Message);
                return false;
            }
        }
    }

    public class Symbol_
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsEnabled { get; set; }
        public string Type { get; set; }
        public int IexId { get; set; }

        public Symbol_()
        {
            this.Symbol = "";
            this.Name = "";
            this.Date = DateTime.UtcNow;
            this.IsEnabled = false;
            this.Type = "";
            this.IexId = 0;
        }

        public int Save_in_Database()
        {
            return this.IEXTrading_Symbol_Insert_Update();
        }

        public int IEXTrading_Symbol_Insert_Update(string connection_string = "")
        {
            string result = "Before";
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("Symbol", this.Symbol);
                param.Add("Name", this.Name);
                param.Add("IsEnabled", this.IsEnabled);
                param.Add("Type", this.Type);
                param.Add("IexId", this.IexId);

                result = Library.Database.ExecuteProcedure_Get(
                    "[fsn].[Symbol_Insert_Update]"
                    , param, connection_string);
                if (result.Contains("\"Result\":0"))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Log_Error("[AustinsFirstProject.StockAdvisor.IEXTrading.Symbol_.IEXTrading_Symbol_Insert_Update]. Result: " + result + "failed. Message: " + ex.Message);
            }

            return 1;

        }
    }
}
