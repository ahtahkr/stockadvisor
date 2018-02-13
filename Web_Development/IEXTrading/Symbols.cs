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
                List<Symbol_> _symbols;
                string directory;

                for (int a = 0; a < SYMBOLS.Count; a++)
                {
                    directory = Utility.Get_Full_FileName_to_Save_Api_Result("", "Symbol");

                    _symbols = new List<Symbol_>
                    {
                        SYMBOLS[a]
                    };
                    File.AppendAllText(directory, JsonConvert.SerializeObject(_symbols));
                }
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

    }
}
