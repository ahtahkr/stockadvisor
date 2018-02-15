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
    /* Gen is a British Slang word for News. */
    public class Gen
    {
        public string Symbol { get; set; }
        public DateTime DateTime { get; set; }
        public string Headline { get; set; }
        public string Source { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
        public string Related { get; set; }
        
        public int Save_in_Database(string connection_string = "")
        {
            string result = "Before";
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("Symbol", this.Symbol);
                param.Add("Date", this.DateTime);
                param.Add("Headline", this.Headline);
                param.Add("Source", this.Source);
                param.Add("Url", this.Url);
                param.Add("Summary", this.Summary);
                param.Add("Related", this.Related);

                result = Library.Database.ExecuteProcedure_Get(
                    "[fsn].[News_Insert_Update]"
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

        public void Set_Symbol(string symbol)
        {
            this.Symbol = symbol;
        }
    }

    public class News
    {
        public List<Symbol_> Symbols { get; set; }

        public List<Gen> Gen { get; set; }

        public bool Api_Called { get; set; } = false;

        public bool Get_Symbols_From_Database(string connection_string = "")
        {
            try
            {
                string result = Library.Database.ExecuteProcedure_Get(
                        "[fsn].[Get_Symbol_For_News]"
                        , null, connection_string);

                this.Symbols = JsonConvert.DeserializeObject<List<Symbol_>>(result);
                return true;
            } catch (Exception ex)
            {
                /* MethodFullName. */
                Logger.Log_Error("[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "] Failed Error Message: " + ex.Message);
            }
            this.Symbols = new List<Symbol_>();
            return false;
        }

        public bool Call_Api()
        {
            this.Gen = new List<Gen>();
            List<Gen> newlist;

            for (int a = 0; a < this.Symbols.Count; a++)
            {
                try
                {
                    newlist = JsonConvert.DeserializeObject<List<Gen>>(
                                            Utility.HttpRequestor.News(this.Symbols[a].Symbol)
                                        );
                    newlist.ForEach(gen => gen.Set_Symbol(this.Symbols[a].Symbol));
                    this.Gen.AddRange(newlist);
                    this.Api_Called = true;
                    Console.WriteLine(a);
                }
                catch (Exception ex)
                {
                    /* MethodFullName. */
                    Logger.Log_Error("[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "] Failed for symbol [" + this.Symbols[a].Symbol + "] Error Message: " + ex.Message);
                }
            }
            
            if (this.Gen.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Save_In_File(string directory = "")
        {
            try
            {

                string full_file_name;

            do
            {
                full_file_name = Utility.Get_Full_FileName_to_Save_Api_Result(directory, "News");
            } while (File.Exists(full_file_name));

            File.AppendAllText(full_file_name, JsonConvert.SerializeObject(this.Gen));
                return true;
            }
            catch (Exception ex)
            {
                /* MethodFullName. */
                Logger.Log_Error("[" + this.GetType().FullName + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "] Failed Error Message: " + ex.Message);
            }
            return false;
        }
    }
}
