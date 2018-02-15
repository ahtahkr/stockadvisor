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
        public DateTime DateTime { get; set; }
        public string Headline { get; set; }
        public string Source { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
        public string Related { get; set; }                
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

            for (int a = 0; a < this.Symbols.Count; a++)
            {
                try
                {
                    this.Gen.AddRange(JsonConvert.DeserializeObject<List<Gen>>(
                                    Utility.HttpRequestor.News(this.Symbols[a].Symbol)));
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
