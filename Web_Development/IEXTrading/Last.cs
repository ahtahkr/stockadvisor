﻿using Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAdvisor.IEXTrading
{
    public class Lasts
    {
        public List<Last> Last { get; set; }
        public bool Api_Called { get; set; }
        public Lasts()
        {
            this.Last = new List<Last>();
            this.Api_Called = false;
        }
        public void Save_In_File(string directory = "")
        {
            if (Api_Called)
            {
                string full_file_name;
                
                do
                {
                    full_file_name = Utility.Get_Full_FileName_to_Save_Api_Result(directory, "Last");
                } while (File.Exists(full_file_name));

                File.AppendAllText(full_file_name, JsonConvert.SerializeObject(this.Last));
                
            } else
            {
                Logger.Log_Error("Save_In_File called without calling Call_Api");
            }
        }

        public bool Call_Api()
        {
            string result = "Before";
            try
            {
                result = Utility.HttpRequestor.Last();

                this.Last = JsonConvert.DeserializeObject<List<Last>>(
                                    result
                            );
                
                this.Api_Called = true;
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log_Error("StockAdvisor.IEXTrading.Lasts.Call_Api() failed. Error Msg: " + ex.Message);
                return false;
            }
        }
    }
    public class Last
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public int Size { get; set; }
        public long Time { get; set; }

        public int Save_in_Database(string connection_string = "")
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            string result = "Before";
            try
            {
                /* Do Not save data if the share close price is way too high. */
                if (this.Price >= 100000)
                {
                    return 0;
                }
                
                param.Add("Symbol", this.Symbol);
                param.Add("Close", this.Price);
                param.Add("Date", (new DateTime(1970,1,1)).AddMilliseconds(this.Time));

                result = Library.Database.ExecuteProcedure_Get(
                    "[fsn].[Share_Insert_Update_Close]"
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
                string s = string.Join(";", param.Select(x => x.Key + "=" + x.Value).ToArray());

                Logger.Log_Error("[StockAdvisor.IEXTrading.Last.Save_in_Database]. Result: " + result + " failed. Parameter: "+s+ "Message: " + ex.Message);
            }

            return 1;

        }
    }
}
